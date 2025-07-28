using System.Text.Json.Serialization;

namespace Web.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MeetingStatusEnum
{
    WaitingForConfirmation,
    Confirmed,
    During,
    Finished,
    Canceled
}