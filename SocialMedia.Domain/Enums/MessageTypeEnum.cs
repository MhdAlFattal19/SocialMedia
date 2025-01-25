namespace SocialMedia.Domain.Enums
{
    public enum MessageTypeEnum
    {
        /// <summary>
        /// Business messages targets normal users.
        /// </summary>
        Business = 1,

        /// <summary>
        /// Technical messages targets technical and support team.
        /// </summary>
        Technical,

        /// <summary>
        /// Information messages targets normal users
        /// </summary>
        Information
    }
}
