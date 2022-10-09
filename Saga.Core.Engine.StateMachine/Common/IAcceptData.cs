namespace Saga.Core.Engine.StateMachine.Abstractions
{
    public interface IAcceptData<TData>
    {
        TData Data { get; }
        void AcceptData(TData data);
    }

    //public interface IAcceptData<TData1, TData2> : IAcceptData<TData1>
    //{
    //    TData2 Data2 { get; }
    //    void AcceptData(TData2 data);
    //}

    //public interface IAcceptData<TData1, TData2, TData3> : IAcceptData<TData1, TData2>
    //{
    //    TData3 Data3 { get; }
    //    void AcceptData(TData3 data);
    //}

    //public interface IAcceptData<TData1, TData2, TData3, TData4> : IAcceptData<TData1, TData2, TData3>
    //{
    //    TData4 Data4 { get; }
    //    void AcceptData(TData4 data);
    //}

    //public interface IAcceptData<TData1, TData2, TData3, TData4, TData5> : IAcceptData<TData1, TData2, TData3, TData4>
    //{
    //    TData5 Data5 { get; }
    //    void AcceptData(TData5 data);
    //}

    //public interface IAcceptData<TData1, TData2, TData3, TData4, TData5, TData6> : IAcceptData<TData1, TData2, TData3, TData4, TData5>
    //{
    //    TData6 Data6 { get; }
    //    void AcceptData(TData6 data);
    //}

    //public interface IAcceptData<TData1, TData2, TData3, TData4, TData5, TData6, TData7> : IAcceptData<TData1, TData2, TData3, TData4, TData5, TData6>
    //{
    //    TData7 Data7 { get; }
    //    void AcceptData(TData7 data);
    //}

    //public interface IAcceptData<TData1, TData2, TData3, TData4, TData5, TData6, TData7, TData8> : IAcceptData<TData1, TData2, TData3, TData4, TData5, TData6, TData7>
    //{
    //    TData8 Data8 { get; }
    //    void AcceptData(TData8 data);
    //}
}