﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using ProtoBuf;

namespace PureFood.Common;

public class Serialize
{
    public static void Init<T>()
    {
        Serializer.PrepareSerializer<T>();
    }

    public static byte[] ProtoBufSerialize(object? item)
    {
        if (item != null)
        {
            try
            {
                var ms = new MemoryStream();
                Serializer.Serialize(ms, item);
                var rt = ms.ToArray();
                return rt;
            }
            catch (ProtoBuf.ProtoException ex)
            {
                throw new Exception("Unable to serialize object:" + item.GetType().FullName + "----" + ex.Message,
                    ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to serialize object:" + item.GetType().FullName + "----" + ex.Message,
                    ex);
            }
        }

        throw new Exception("Object serialize is null:" + item?.GetType().FullName);
    }

    public static byte[] ProtoBufSerialize(object? item, bool isCompress)
    {
        if (item != null)
        {
            try
            {
                var ms = new MemoryStream();
                Serializer.Serialize(ms, item);
                var rt = ms.ToArray();
                if (isCompress)
                {
                    rt = Compress(rt);
                }

                return rt;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to serialize object:" + item.GetType().FullName + "----" + ex.Message,
                    ex);
            }
        }

        throw new Exception("Object serialize is null:" + item?.GetType().FullName);
    }

    public static Stream ProtoBufSerializeToStream(object? item, bool isCompress)
    {
        if (item != null)
        {
            try
            {
                var ms = new MemoryStream();
                Serializer.Serialize(ms, item);

                if (isCompress)
                {
                    var rt = ms.ToArray();
                    return CompressToStream(rt);
                }
                else
                {
                    return ms;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to serialize object:" + item.GetType().FullName + "----" + ex.Message,
                    ex);
            }
        }

        throw new Exception("Object serialize is null:" + item?.GetType().FullName);
    }

    public static T? ProtoBufDeserialize<T>(byte[]? byteArray)
    {
        if (byteArray != null && byteArray.Length > 0)
        {
            try
            {
                var ms = new MemoryStream(byteArray);
                return Serializer.Deserialize<T>(ms);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to deserialize object" + typeof(T).FullName, ex);
                //return default(T);
            }
        }

        throw new Exception("Object Deserialize is null or empty");
        //return default(T);
    }

    public static T? ProtoBufDeserialize<T>(byte[]? byteArray, bool isDecompress)
    {
        if (byteArray != null && byteArray.Length > 0)
        {
            try
            {
                if (isDecompress)
                {
                    byteArray = Decompress(byteArray);
                }

                return ProtoBufDeserialize<T>(byteArray);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to deserialize object" + typeof(T).FullName, ex);
            }
        }

        throw new Exception("Object Deserialize is null or empty");
        //return default(T);
    }

    public static object? ProtoBufDeserialize(byte[]? byteArray, Type type)
    {
        if (byteArray is { Length: > 0 })
        {
            try
            {
                var ms = new MemoryStream(byteArray);
                return Serializer.Deserialize(type, ms);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to deserialize object:{type.FullName}---{ex.Message}", ex);
                //return default(T);
            }
        }

        throw new Exception("Object Deserialize is null or empty");
        //return default(T);
    }

    public static object? ProtoBufDeserialize(byte[]? byteArray, Type type, bool isDecompress)
    {
        if (byteArray != null && byteArray.Length > 0)
        {
            try
            {
                if (isDecompress)
                {
                    byteArray = Decompress(byteArray);
                }

                return ProtoBufDeserialize(byteArray, type);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to deserialize object:{type.FullName}---{ex.Message}", ex);
                //return default(T);
            }
        }

        throw new Exception("Object Deserialize is null or empty");
        //return default(T);
    }

    public static byte[] Compress(byte[] raw)
    {
        using MemoryStream memory = new MemoryStream();
        using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
        {
            gzip.Write(raw, 0, raw.Length);
        }

        return memory.ToArray();
    }

    public static byte[]? Compress(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }

        byte[] raw = Encoding.UTF8.GetBytes(text);
        return Compress(raw);
    }

    public static Stream CompressToStream(byte[] raw)
    {
        using (MemoryStream memory = new MemoryStream())
        {
            using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
            {
                gzip.Write(raw, 0, raw.Length);
            }

            return memory;
        }
    }

    public static byte[] Decompress(byte[] gzip)
    {
        // Create a GZIP stream with decompression mode.
        // ... Then create a buffer and write into while reading from the GZIP stream.
        using GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress);
        const int size = 4096;
        byte[] buffer = new byte[size];
        using MemoryStream memory = new MemoryStream();
        int count = 0;
        do
        {
            count = stream.Read(buffer, 0, size);
            if (count > 0)
            {
                memory.Write(buffer, 0, count);
            }
        } while (count > 0);

        return memory.ToArray();
    }

    public static string? DecompressToString(byte[]? gzip)
    {
        if (gzip == null || gzip.Length <= 0)
        {
            return null;
        }

        var data = Decompress(gzip);
        return Encoding.UTF8.GetString(data);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string JsonSerializeObject<T>(T obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }

        return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
    }

    public static T? JsonDeserializeObject<T>(string? json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return default(T);
        }

        try
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.None,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Arrays,
            };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
        catch (Exception e)
        {
            return default(T);
        }
    }
    public static object? JsonDeserializeObject(string json, Type type)
    {
        return JsonConvert.DeserializeObject(json, type);
    }

    public static object? JsonDeserializeObject(string json)
    {
        return JsonConvert.DeserializeObject(json);
    }
    //[Trace]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string JsonSerializeObjectJsonIgnore<T>(object? obj)
    {
        if (obj == null)
        {
            return string.Empty;
        }

        return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new JsonIgnoreResolver<T>(),
                DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
            });
    }

    public class JsonIgnoreResolver<T> : CamelCasePropertyNamesContractResolver
    {
        // protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        // {
        //     IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
        //     return props.Where(p => p.Writable).ToList();
        // }
        // protected override IList<JsonProperty> CreateProperties1(Type type, MemberSerialization memberSerialization)
        // {
        //     var list =  type.GetProperties()
        //         .Where(x => !x.GetCustomAttributes().Any(a => a.GetType() == _AttributeToIgnore))
        //         .Select(p => new JsonProperty()
        //         {
        //             PropertyName = p.Name,
        //             PropertyType = p.PropertyType,
        //             Readable = true,
        //             Writable = true,
        //             ValueProvider = base.CreateMemberValueProvider(p)
        //         }).ToList();
        //
        //     return list;
        // }
        protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            // list the properties to ignore
            var propertiesToIgnore = type.GetProperties()
                .Where(x => x.GetCustomAttributes().OfType<T>().Any());
            // Build the properties list
            var properties = base.CreateProperties(type, memberSerialization);
            // only serialize properties that are not ignored
            properties = properties
                .Where(p => propertiesToIgnore.All(info => info.Name != p.UnderlyingName))
                .ToList();
            return properties;
        }
    }

    public static bool IsValidJson(string? strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput)) return false;
        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
        {
            return true;
        }

        return false;
    }
}
