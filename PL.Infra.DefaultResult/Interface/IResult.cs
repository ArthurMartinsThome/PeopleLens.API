using PL.Infra.DefaultResult.Model.Enum;
using System.Net;

namespace PL.Infra.DefaultResult.Interface
{
    public interface IResult
    {
        void FinalizeResult(/*int? userId = null, LogLevel? overrideLogLevel = null*/);
    }

    public interface IResult<T> : IResult
    {
        bool Succeded { get; }
        bool HasData { get; }
        string? LastMessage { get; }
        string? PrivateMessage { get; }
        HttpStatusCode? StatusCode { get; }
        T? Data { get; }
        IResult<T> AddMessage(string message);
        IResult<TNew> ChangeType<TNew>(TNew? data);
        public void AddTransactionalLog<TLog>(/*EModule module, */int objectId, EAction action, TLog obj, EOrigin? overrideOrigin = null, EEvent? overrideEventId = null, int? referenceId = null, int? referenceModule = null);
        public void AddTransactionalLog<TLog>(/*EModule module, */int objectId, EAction action, TLog? oldObject, TLog? newObject, EOrigin? overrideOrigin = null, EEvent? overrideEventId = null, int? referenceId = null, int? referenceModule = null);
    }
}