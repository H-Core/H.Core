using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using H.Core.Converters;
using H.Core.Recorders;

namespace H.Core.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseManager : ParentRecorder
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public new IRecorder? Recorder {
            get => base.Recorder;
            set {
                if (value == null && base.Recorder != null)
                {
                    base.Recorder.Started -= Recorder_OnStarted;
                    base.Recorder.Stopped -= Recorder_OnStopped;
                }

                base.Recorder = value;

                if (base.Recorder != null)
                {
                    base.Recorder.Started += Recorder_OnStarted;
                    base.Recorder.Stopped += Recorder_OnStopped;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IConverter? Converter { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<IConverter> AlternativeConverters { get; } = new ();

        /// <summary>
        /// 
        /// </summary>
        public string? Text { get; private set; }

        #endregion

        #region Events
        
        /// <summary>
        /// 
        /// </summary>

        public event EventHandler<string?>? NewText;
        private void OnNewText() => NewText?.Invoke(this, Text);

        #endregion

        #region Constructors

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public void ProcessText(string text)
        {
            Text = text;
            OnNewText();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ProcessSpeechAsync(byte[] bytes, CancellationToken cancellationToken = default)
        {
            if (Converter == null)
            {
                OnLogReceived("Converter is not found");
                return;
            }

            try
            {
                if (Converter.IsStreamingRecognitionSupported)
                {
                    return;
                }

                var text = await Converter.ConvertAsync(bytes, cancellationToken).ConfigureAwait(false);
                if (!AlternativeConverters.Any())
                {
                    //Log("No alternative converters");
                    ProcessText(text);
                    return;
                }

                var alternativeTexts = AlternativeConverters.Select(async i => await i.ConvertAsync(bytes, cancellationToken).ConfigureAwait(false)).ToList();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    //Log("Text is not empty. No alternative converters is uses");
                    ProcessText(text);
                    return;
                }

                //Log("Loop");
                while (alternativeTexts.Any())
                {
                    //Log("WhenAny");
                    var alternativeTextTask = await Task.WhenAny(alternativeTexts).ConfigureAwait(false);
                    var alternativeText = await alternativeTextTask.ConfigureAwait(false);
                    if (string.IsNullOrWhiteSpace(alternativeText))
                    {
                        //Log("string.IsNullOrWhiteSpace");
                        alternativeTexts.Remove(alternativeTextTask);
                        continue;
                    }

                    //Log("ProcessText");
                    ProcessText(text);
                    return;
                }

                //Log("ProcessOriginalText");
                ProcessText(text);
            }
            catch (Exception exception)
            {
                OnLogReceived($"{exception}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StartAsync(CancellationToken cancellationToken = default)
        {
            if (IsStarted)
            {
                return;
            }

            Text = null;
            await base.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken = default)
        {
            if (!IsStarted)
            {
                return;
            }

            if (Recorder == null)
            {
                OnLogReceived("Recorder is not found");
                return;
            }

            await Recorder.StopAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ChangeAsync(CancellationToken cancellationToken = default)
        {
            if (!IsStarted)
            {
                await StartAsync(cancellationToken).ConfigureAwait(false);
            }
            else
            {
                await StopAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region Event handlers

        private async void Recorder_OnStarted(object sender, EventArgs args)
        {
            if (Recorder == null)
            {
                OnLogReceived("Recorder is not found");
                return;
            }

            if (Converter == null ||
                !Converter.IsStreamingRecognitionSupported)
            {
                return;
            }

            using var recognition = await Converter.StartStreamingRecognitionAsync().ConfigureAwait(false);
            recognition.PartialResultsReceived += (_, value) => ProcessText($"deskband preview {value}");
            recognition.FinalResultsReceived += (_, value) =>
            {
                ProcessText("deskband clear-preview");
                ProcessText(value);
            };

            if (Recorder.RawData.Any())
            {
                await recognition.WriteAsync(Recorder.RawData).ConfigureAwait(false);
            }

            // ReSharper disable once AccessToDisposedClosure
            async void RecorderOnRawDataReceived(object o, byte[] bytes)
            {
                await recognition.WriteAsync(bytes).ConfigureAwait(false);
            }

            try
            {
                Recorder.RawDataReceived += RecorderOnRawDataReceived;

                while (Recorder.IsStarted)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                }

                await recognition.StopAsync().ConfigureAwait(false);
            }
            finally
            {
                Recorder.RawDataReceived -= RecorderOnRawDataReceived;
            }
        }

        private async void Recorder_OnStopped(object sender, EventArgs args)
        {
            IsStarted = false;

            if (Recorder == null)
            {
                OnLogReceived("Recorder is not found");
                return;
            }

            RawData = Recorder.RawData;
            WavData = Recorder.WavData;
            OnStopped();

            if (!WavData.Any())
            {
                return;
            }

            await ProcessSpeechAsync(WavData).ConfigureAwait(false);
        }

        #endregion

        #region IDisposable

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            Recorder?.Dispose();
            Converter?.Dispose();

            base.Dispose();
        }

        #endregion
    }
}
