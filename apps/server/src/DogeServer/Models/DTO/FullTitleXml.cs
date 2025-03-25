using System.Xml.Serialization;

namespace DogeServer.Models.DTO;

[XmlRoot("ECFR")]
public class FullTitleXml
{
    [XmlElement("AMDDATE")] public string?  AmendmentDate   { get; set; }
    [XmlElement("VOLUME")]  public Volume?  Volume          { get; set; }
    [XmlElement("DIV1")]    public Div?     Title           { get; set; }
}

public class Volume
{
    [XmlElement("DIV1")]    public Div?     Title   { get; set; }
    [XmlElement("NUM")]     public string?  Number  { get; set; }
    [XmlElement("DATE")]    public string?  Date    { get; set; }
    [XmlElement("SUBJECT")] public string?  Subject { get; set; }
}

public class Div
{
    [XmlElement("HEAD")]    public string?          Header      { get; set; }
    [XmlElement("HED")]     public string?          Type        { get; set; }
    [XmlElement("AUTH")]    public Div?             Auth        { get; set; }
    [XmlElement("PSPACE")]  public string?          Pspace      { get; set; }
    [XmlElement("SOURCE")]  public Div?             Source      { get; set; }
    [XmlElement("CITA")]    public string?          Citation    { get; set; }
    [XmlElement("EDNOTE")]  public string?          Ednote      { get; set; }
    [XmlElement("DIV2")]    public List<Div>?       Div2        { get; set; }
    [XmlElement("DIV3")]    public List<Div>?       Chapter     { get; set; }
    [XmlElement("DIV4")]    public List<Div>?       Subchapter  { get; set; }
    [XmlElement("DIV5")]    public List<Div>?       Part        { get; set; }
    [XmlElement("DIV6")]    public List<Div>?       Subpart     { get; set; }
    [XmlElement("DIV7")]    public List<Div>?       Section     { get; set; }
    [XmlElement("DIV8")]    public List<Div>?       Div8        { get; set; }
    [XmlElement("P")]       public List<Paragraph>? Paragraphs  { get; set; }
}

public class Paragraph
{
    [XmlText]           public string?          Text    { get; set; }
    [XmlElement("I")]   public List<string>?    Italics { get; set; }
}