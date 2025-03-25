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

public class Paragraph
{
    [XmlText]               public string?          Text        { get; set; }
    [XmlElement("E")]       public List<string>?    Emphasis    { get; set; } //RISK
    [XmlElement("PSPACE")]  public string?          Pspace      { get; set; }
    [XmlElement("I")]       public List<string>?    Italics     { get; set; } //RISK
    [XmlElement("HEAD")]    public string?          Header      { get; set; }
    [XmlElement("HED")]     public string?          Hed         { get; set; }
    [XmlElement("P")]       public List<string>?    Paragraphs  { get; set; } //RISK
}

public class Div : Paragraph
{
    [XmlElement("SOURCE")]  public Div?             Source      { get; set; }
    [XmlElement("EDNOTE")]  public Div?             Ednote      { get; set; }

    [XmlElement("DIV2")]    public List<Div>?       Div2        { get; set; }
    [XmlElement("DIV3")]    public List<Div>?       Chapter     { get; set; }
    [XmlElement("DIV4")]    public List<Div>?       Subchapter  { get; set; }
    [XmlElement("DIV5")]    public List<Div>?       Part        { get; set; }
    [XmlElement("DIV6")]    public List<Div>?       Subpart     { get; set; }
    [XmlElement("DIV7")]    public List<Div>?       Section     { get; set; }
    [XmlElement("DIV8")]    public List<Div>?       Div8        { get; set; }
    [XmlElement("AUTH")]    public Paragraph?       Auth        { get; set; } //RISK
    
    [XmlElement("N")]       public string?          Num         { get; set; }
    [XmlElement("TYPE")]    public string?          Type        { get; set; }
    [XmlElement("VOLUME")]  public string?          Volume      { get; set; }
}
