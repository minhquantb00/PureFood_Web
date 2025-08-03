using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.Common;

public static class FileUtility
{
    public static string ComputeSha256(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }

        using var stream = File.OpenRead(filePath);
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    public static string GetThumbnailUrl(string? extension, bool isVideo = false)
    {
        var result = "File/ViewIcon/";
        if (isVideo)
        {
            return result + "video.png";
        }

        switch (extension)
        {
            case ".ppt":
                result += "ppt.png";
                break;
            case ".doc":
            case ".docx":
                result += "doc.png";
                break;
            case ".xls":
            case ".xlsx":
                result += "excel.png";
                break;
            case ".pdf":
                result += "pdf.png";
                break;
            default:
                result += "other.png";
                break;
        }

        return result;
    }

    public static async Task<string> SaveFile(string fileName, byte[] fileBytes, string folderPath)
    {
        try
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fullPath = Path.Combine(folderPath, fileName);
            await File.WriteAllBytesAsync(fullPath, fileBytes);
            return fullPath;
        }
        catch (Exception ex)
        {
            throw new Exception("Save file error", ex);
        }
    }

    public static string GetFileContentType(string extension)
    {
        extension = extension.ToLower();
        var fileMimeTypes = new Dictionary<string, string>
        {
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".bmp", "image/bmp" },
            { ".webp", "image/webp" },
            { ".svg", "image/svg+xml" },
            { ".ico", "image/x-icon" },
            { ".tif", "image/tiff" },
            { ".tiff", "image/tiff" },
            { ".heic", "image/heic" },
            { ".heif", "image/heif" },
            { ".jfif", "image/jpeg" },
            { ".pjpeg", "image/pjpeg" },
            { ".pjp", "image/pjpeg" },
            { ".avif", "image/avif" },
            { ".raw", "image/x-raw" },
            { ".cr2", "image/x-canon-cr2" },
            { ".nef", "image/x-nikon-nef" },
            { ".orf", "image/x-olympus-orf" },
            { ".sr2", "image/x-sony-sr2" },
            // VIDEO
            { ".mp4", "video/mp4" },
            { ".m4v", "video/mp4" },
            { ".mov", "video/quicktime" },
            { ".wmv", "video/x-ms-wmv" },
            { ".avi", "video/x-msvideo" },
            { ".flv", "video/x-flv" },
            { ".mkv", "video/x-matroska" },
            { ".webm", "video/webm" },
            { ".3gp", "video/3gpp" },
            { ".3g2", "video/3gpp2" },
            { ".ts", "video/mp2t" },
            { ".ogv", "video/ogg" },
            // FILE NÉN
            { ".zip", "application/zip" },
            { ".rar", "application/vnd.rar" },
            { ".7z", "application/x-7z-compressed" },
            // TÀI LIỆU
            { ".pdf", "application/pdf" },
            { ".doc", "application/msword" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".ppt", "application/vnd.ms-powerpoint" },
            { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },

            // ÂM THANH
            { ".mp3", "audio/mpeg" },
            { ".wav", "audio/wav" },
            { ".ogg", "audio/ogg" },
            { ".m4a", "audio/mp4" },
            { ".flac", "audio/flac" },

            // LẬP TRÌNH / CẤU HÌNH
            { ".html", "text/html" },
            { ".css", "text/css" },
            { ".js", "application/javascript" },
            { ".yml", "application/x-yaml" },
            { ".yaml", "application/x-yaml" },
            { ".txt", "text/plain" },
            { ".ini", "text/plain" },
            { ".log", "text/plain" }
        };

        if (!string.IsNullOrWhiteSpace(extension) && fileMimeTypes.TryGetValue(extension, out var contentType))
        {
            return contentType;
        }

        return "application/octet-stream";
    }
}
