namespace Spoticry.Application.Conta.Dto
{
    public class PlaylistDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Boolean Publica { get; set; }
        public List<MusicaDto> Musicas { get; set; }

    }
}
