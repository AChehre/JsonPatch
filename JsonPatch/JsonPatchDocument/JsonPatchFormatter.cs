using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using JsonPatch.Extentions;
using Newtonsoft.Json;

namespace JsonPatch
{
    public class JsonPatchFormatter : BufferedMediaTypeFormatter
    {
        public JsonPatchFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json-patch+json"));
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override bool CanReadType(Type type)
        {
            if (type == typeof(JsonPatchDocument))
                return true;
            return false;
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content,
            IFormatterLogger formatterLogger,
            CancellationToken cancellationToken)
        {
            return ReadFromStream(type, readStream, content, formatterLogger);
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content,
            IFormatterLogger formatterLogger)
        {
            using (var reader = new StreamReader(readStream))
            {
                var jsonPatchDocument = new JsonPatchDocument();

                var jsonString = reader.ReadToEnd();
                var operations = JsonConvert.DeserializeObject<PatchOperation[]>(jsonString);

                foreach (var operation in operations)
                {
                    var jsonPatchOperation = new JsonPatchOperation
                    {
                        FromPath = operation.from,
                        Path = operation.path,
                        Value = operation.value
                    };

                    if (operation.op == JsonPatchOperationType.Add.GetDescription())
                        jsonPatchOperation.Operation = JsonPatchOperationType.Add;

                    if (operation.op == JsonPatchOperationType.Copy.GetDescription())
                        jsonPatchOperation.Operation = JsonPatchOperationType.Copy;

                    if (operation.op == JsonPatchOperationType.Move.GetDescription())
                        jsonPatchOperation.Operation = JsonPatchOperationType.Move;

                    if (operation.op == JsonPatchOperationType.Remove.GetDescription())
                        jsonPatchOperation.Operation = JsonPatchOperationType.Remove;

                    if (operation.op == JsonPatchOperationType.Replace.GetDescription())
                        jsonPatchOperation.Operation = JsonPatchOperationType.Replace;

                    if (operation.op == JsonPatchOperationType.Test.GetDescription())
                        jsonPatchOperation.Operation = JsonPatchOperationType.Test;

                    jsonPatchDocument.Add(jsonPatchOperation);
                }

                return jsonPatchDocument;
            }
        }
    }
}