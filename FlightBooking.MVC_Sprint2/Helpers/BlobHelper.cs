using Azure;
using Azure.Storage.Blobs;
using FlightBooking.MVC_Sprint2.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightBooking.MVC_Sprint2.Helpers
{
    public class BlobHelper
    {
        public static async Task<bool> UploadBlob(
     IConfiguration config,
     TicketDto ticket)
        {
            string blobConnString = config.GetConnectionString("StorAccConnString");
            BlobServiceClient client = new BlobServiceClient(blobConnString);
            string container = config.GetValue<string>("Container");
            var containerClient = client.GetBlobContainerClient(container);


            // Create a local file in the ./data/ directory for uploading and downloading
            string localPath = "./files/";
            string fileName = "ofm.ticket." + Guid.NewGuid().ToString() + ".json";
            string localFilePath = Path.Combine(localPath, fileName);


            // Write text to the file
            await System.IO.File.WriteAllTextAsync(localFilePath, JsonConvert.SerializeObject(ticket));


            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);


            // Upload data from the local file
            try
            {
                await blobClient.UploadAsync(localFilePath, true);
            }
            catch (RequestFailedException ex)
            {
                return false;
            }

            System.IO.File.Delete(localFilePath);


            return true;
        }
    }
}
