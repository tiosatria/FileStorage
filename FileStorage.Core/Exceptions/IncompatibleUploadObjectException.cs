
using FileStorage.Abstraction.Contracts;

namespace FileStorage.Core.Exceptions
{
    public class IncompatibleUploadObjectException<TExpected> :
        FileStorageException
        where TExpected : IUploadStorageObject
    {

        static string MessageTemplate(IUploadStorageObject obj, out string expected, out string given)
        {
            expected = typeof(TExpected).Name;
            given = obj.GetType().Name;
            return $"Incompatible upload object detected. Expected Type of upload object: {expected} but instead, given : {given}";
        }

        public IncompatibleUploadObjectException(IUploadStorageObject obj) : base(msg: MessageTemplate(obj,out var expected, out var given))
        {
            ExpectedTypeString = expected;
            GivenTypeString = given;
        }

        public string ExpectedTypeString { get; init; }
        public string GivenTypeString { get; init; }

    }


    public class IncompatibleUploadObjectException<TExpected,TGiven> :
        FileStorageException
    where TExpected : IUploadStorageObject
    where TGiven : IUploadStorageObject
    {

        static string MessageTemplate(out string expected, out string given)
        {
            expected = typeof(TExpected).Name;
            given = typeof(TGiven).Name;
            return $"Incompatible upload object detected. Expected Type of upload object: {expected} but instead, given : {given}";
        }

        public IncompatibleUploadObjectException():base(msg:MessageTemplate(out var expected, out var given))
        {
            ExpectedTypeString = expected;
            GivenTypeString = given;
        }

        public string ExpectedTypeString { get; init; }
        public string GivenTypeString { get; init; }

    }
}
