using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.Common;

public class MultipartRequestUtility
{
    public async Task<byte[]> StreamToBytes(Stream input)
    {
        byte[] buffer = new byte[16 * 1024];
        using MemoryStream ms = new MemoryStream();
        int read;
        while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            await ms.WriteAsync(buffer, 0, read);
        }

        return ms.ToArray();
    }

    public string GetBoundary(Microsoft.Net.Http.Headers.MediaTypeHeaderValue contentType, int lengthLimit)
    {
        var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
        if (!boundary.HasValue)
        {
            throw new InvalidDataException("Missing content-type boundary.");
        }

        if (boundary.Length > lengthLimit)
        {
            throw new InvalidDataException(
                $"Multipart boundary length limit {lengthLimit} exceeded.");
        }

        return boundary.Value;
    }

    public object GetBoundary(Microsoft.Net.Http.Headers.MediaTypeHeaderValue mediaTypeHeaderValue, object multipartBoundaryLengthLimit)
    {
        throw new NotImplementedException();
    }

    public bool IsMultipartContentType(string contentType)
    {
        return !string.IsNullOrEmpty(contentType)
               && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public bool HasFormDataContentDisposition(Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition)
    {
        // Content-Disposition: form-data; name="key";
        return contentDisposition != null
               && contentDisposition.DispositionType.Equals("form-data")
               && !contentDisposition.FileName.HasValue
               && !contentDisposition.FileNameStar.HasValue;
    }

    public bool HasFileContentDisposition(Microsoft.Net.Http.Headers.ContentDispositionHeaderValue contentDisposition)
    {
        // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
        return contentDisposition != null
               && contentDisposition.DispositionType.Equals("form-data")
               && (contentDisposition.FileName.HasValue
                   || contentDisposition.FileNameStar.HasValue);
    }
}
