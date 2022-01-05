using System.ServiceModel;

namespace WebJwtRestApi.Controllers
{
    [ServiceContract]
    internal interface IService1
    {
        [OperationContract]
        string GetData1();

        [OperationContract]
        string GetData(int value);


        [OperationContract]
        byte[] KreirajReport1(string nazivRdlc, string jsonSt1, string jsonSt2);

        [OperationContract]
        byte[] KreirajReport(string nazivRdlc, string jsonDataSet, string jsonParameters);

    }
}