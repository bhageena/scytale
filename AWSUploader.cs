using Amazon.S3;
using Amazon.S3.Transfer;
using log4net;
using System.Configuration;
using System.IO;
namespace Scytale
{
    class AWSUploader
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AWSUploader));
        string bucketName = ConfigurationManager.AppSettings["AWSBucket"];
        string keyName = ConfigurationManager.AppSettings["AWSAccessKey"];

      public  void Upload(string filePath)
        {
            try
            {
                TransferUtility fileTransferUtility = new TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.USEast1));
                using (FileStream fileToUpload = new FileStream(filePath, FileMode.Open, FileAccess.Read)) 
                {
                    fileTransferUtility.Upload(fileToUpload, bucketName, keyName);
                }
            }
            catch (AmazonS3Exception ex)
            {
                Log.Error(ex.Message, ex.InnerException);
            }
        }
    }
}