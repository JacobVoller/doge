# Flow: Seed

## Diagram

## [sequencediagram.org](http://sequencediagram.org/)

<pre>
fontawesome6solid f233 Server #blue
fontawesome6solid f233 Database #blue
fontawesome6solid f360 eCFR #red

==Seed==
activate Server

Server -> eCFR:**Get All Titles**\n--///titles.json//--
eCFR --> Server: Title[]

aboxleft right of Server:foreach title
activate Server
Server -> eCFR:**Get Structure of Title**\n/--//structure/{date}/title-{title}.json//--
eCFR --> Server: TitleStructure
Server -:1>(1) Database: insert(title)
Server -:1>(1) Database: insert(chapter)
Server -:1>(1) Database: insert(subchapter)
Server -:1>(1) Database: insert(part)
Server -:1>(1) Database: insert(section)

Server -> eCFR:**Get Title Content**\n--//full/{date}/title-{title}.xml//--
eCFR --> Server: TitleContent
Server -:1>(1) Database: insert(regulation)

deactivate Server
deactivate Server
</pre>