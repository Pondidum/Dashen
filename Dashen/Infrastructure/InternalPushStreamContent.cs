using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dashen.Infrastructure
{

	public class InternalPushStreamContent : HttpContent
	{
		internal class CompleteTaskOnCloseStream : Stream
		{

			private TaskCompletionSource<bool> _serializeToStreamTask;
			public CompleteTaskOnCloseStream(Stream innerStream, TaskCompletionSource<bool> serializeToStreamTask)
				: this(innerStream)
			{
				this._serializeToStreamTask = serializeToStreamTask;
			}
			public override void Close()
			{
				this._serializeToStreamTask.TrySetResult(true);
			}

			private Stream _innerStream;
			protected Stream InnerStream
			{
				get
				{
					return this._innerStream;
				}
			}
			public override bool CanRead
			{
				get
				{
					return this._innerStream.CanRead;
				}
			}
			public override bool CanSeek
			{
				get
				{
					return this._innerStream.CanSeek;
				}
			}
			public override bool CanWrite
			{
				get
				{
					return this._innerStream.CanWrite;
				}
			}
			public override long Length
			{
				get
				{
					return this._innerStream.Length;
				}
			}
			public override long Position
			{
				get
				{
					return this._innerStream.Position;
				}
				set
				{
					this._innerStream.Position = value;
				}
			}
			public override int ReadTimeout
			{
				get
				{
					return this._innerStream.ReadTimeout;
				}
				set
				{
					this._innerStream.ReadTimeout = value;
				}
			}
			public override bool CanTimeout
			{
				get
				{
					return this._innerStream.CanTimeout;
				}
			}
			public override int WriteTimeout
			{
				get
				{
					return this._innerStream.WriteTimeout;
				}
				set
				{
					this._innerStream.WriteTimeout = value;
				}
			}
			protected CompleteTaskOnCloseStream(Stream innerStream)
			{
				if (innerStream == null)
				{
					throw new ArgumentNullException("innerStream");
				}
				this._innerStream = innerStream;
			}
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this._innerStream.Dispose();
				}
				base.Dispose(disposing);
			}
			public override long Seek(long offset, SeekOrigin origin)
			{
				return this._innerStream.Seek(offset, origin);
			}
			public override int Read(byte[] buffer, int offset, int count)
			{
				return this._innerStream.Read(buffer, offset, count);
			}
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this._innerStream.BeginRead(buffer, offset, count, callback, state);
			}
			public override int EndRead(IAsyncResult asyncResult)
			{
				return this._innerStream.EndRead(asyncResult);
			}
			public override int ReadByte()
			{
				return this._innerStream.ReadByte();
			}
			public override void Flush()
			{
				this._innerStream.Flush();
			}
			public override void SetLength(long value)
			{
				this._innerStream.SetLength(value);
			}
			public override void Write(byte[] buffer, int offset, int count)
			{
				this._innerStream.Write(buffer, offset, count);
			}
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				return this._innerStream.BeginWrite(buffer, offset, count, callback, state);
			}
			public override void EndWrite(IAsyncResult asyncResult)
			{
				this._innerStream.EndWrite(asyncResult);
			}
			public override void WriteByte(byte value)
			{
				this._innerStream.WriteByte(value);
			}
		}


		private readonly Action<Stream, HttpContent, TransportContext> _onStreamAvailable;

		public InternalPushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable)
			: this(onStreamAvailable, (MediaTypeHeaderValue)null)
		{
		}

		public InternalPushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable, string mediaType)
			: this(onStreamAvailable, new MediaTypeHeaderValue(mediaType))
		{
		}

		public InternalPushStreamContent(Action<Stream, HttpContent, TransportContext> onStreamAvailable, MediaTypeHeaderValue mediaType)
		{
			if (onStreamAvailable == null)
			{
				throw new ArgumentNullException("onStreamAvailable");
			}
			this._onStreamAvailable = onStreamAvailable;
			base.Headers.ContentType = mediaType ?? ApplicationOcetStream;
		}

		private static readonly MediaTypeHeaderValue ApplicationOcetStream = new MediaTypeHeaderValue("application/octet-stream");

		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
			try
			{
				Stream arg = new InternalPushStreamContent.CompleteTaskOnCloseStream(stream, taskCompletionSource);
				this._onStreamAvailable(arg, this, context);
			}
			catch (Exception exception)
			{
				taskCompletionSource.TrySetException(exception);
			}
			return taskCompletionSource.Task;
		}

		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}
	}
}
