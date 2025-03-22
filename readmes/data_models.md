# Data Models

## Regulations

<pre>
.
└── Title
    └── Chapter
        └── Subchapter
            └── Part
                └── Section
</pre>

## Call Flow

<pre>
.
└── GetAllTitles (titles.json)
    └── [N] GetTitleStructure (structure/{date}/title-{title}.json)
        └── GetTitleContent (full/{date}/title-{title}.xml)
</pre>
