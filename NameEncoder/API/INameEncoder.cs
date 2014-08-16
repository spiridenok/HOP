namespace HOP.NameEncoder.API
{
    interface INameEncoder
    {
        string Encode( string str );

        string Decode( string str );
    }
}
