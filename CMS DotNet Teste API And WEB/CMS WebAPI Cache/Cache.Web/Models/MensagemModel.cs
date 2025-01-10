namespace Cache.Web.Models;

public class MensagemModel
{
    //public MensagemModel()
    //{
    //    Id = Guid.NewGuid();
    //    DtHrRegistro = DateTime.UtcNow;
    //}

    //public MensagemModel(string idMsgJdPi, string idMsg, string tpMsg, string queueMsg, string xmlMsg) : this()
    //{
    //    IdMsgJdPi = idMsgJdPi;
    //    IdMsg = idMsg;
    //    TpMsg = tpMsg;
    //    QueueMsg = queueMsg;
    //    XmlMsg = xmlMsg;
    //}

    public Guid Id { get; set; }
    public string IdMsgJdPi { get; set; } = string.Empty;
    public string IdMsg { get; set; } = string.Empty;
    public string TpMsg { get; set; } = string.Empty;
    public string QueueMsg { get; set; } = string.Empty;
    public string XmlMsg { get; set; } = string.Empty;
    public DateTime DtHrRegistro { get; set; }
}
