namespace Gouda.Communication.Enumerations
{
    public enum CommunicableType
    {
        Acknowledgement,
        Error,
        Request,
        DataSeries,
        DataSet,

        Mock = int.MaxValue,
    };
}
