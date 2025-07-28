using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Domain.Enums;

namespace Web.Domain.TechnicalStuff.Converters;

public class MeetingStatusEnumConverter()
    : ValueConverter<MeetingStatusEnum, string>(v => ToDatabaseValue(v),
    v => FromDatabaseValue(v))
{
    private static string ToDatabaseValue(MeetingStatusEnum status)
    {
        return status switch
        {
            MeetingStatusEnum.WaitingForConfirmation => "waiting_for_confirmation",
            MeetingStatusEnum.Confirmed => "confirmed",
            MeetingStatusEnum.During => "during",
            MeetingStatusEnum.Finished => "finished",
            MeetingStatusEnum.Canceled => "canceled",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }

    private static MeetingStatusEnum FromDatabaseValue(string value)
    {
        return value switch
        {
            "waiting_for_confirmation" => MeetingStatusEnum.WaitingForConfirmation,
            "confirmed" => MeetingStatusEnum.Confirmed,
            "during" => MeetingStatusEnum.During,
            "finished" => MeetingStatusEnum.Finished,
            "canceled" => MeetingStatusEnum.Canceled,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}
