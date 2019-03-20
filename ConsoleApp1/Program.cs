using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Program
    {
        static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            var blogClient = storageAccount.CreateCloudBlobClient();
            var container = blogClient.GetContainerReference("images");

            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);


            //Code to upload
            //var blockBlob = container.GetBlockBlobReference("backus.png");

            //using (var fileStream = System.IO.File.OpenRead(@"C:\backus.png"))
            //{
            //    blockBlob.UploadFromStream(fileStream);
            //}

            //Code to list
            var blobs = container.ListBlobs();

            foreach(var blob in blobs)
            {
                Console.WriteLine(blob.Uri);
            }

            //upload a file
            //var blockBlob = container.GetBlockBlobReference("backus.png");

            //using (var fileStream = System.IO.File.OpenWrite(@"C:\donwloadbackus.png"))
            //{
            //   blockBlob.DownloadToStream(fileStream);
            //}


            //copy async
            var blockBlob = container.GetBlockBlobReference("backus.png");
            var blockBlobCopy = container.GetBlockBlobReference("molitalia.png");

            var cb = new AsyncCallback(x => Console.WriteLine("Block blob completed"));
            blockBlobCopy.BeginStartCopy(blockBlob.Uri, cb, null);

            Console.ReadKey();
        }
    }
}
