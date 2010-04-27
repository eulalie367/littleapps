<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="table">
    <xsl:apply-templates select="tr" />
  </xsl:template>

  <xsl:template match="tr">
    <xsl:if test="@align != 'center'">
        <xsl:apply-templates select="td/div" />
    </xsl:if>
  </xsl:template>
  <xsl:template match="td/div">
    <xsl:if test="position() = 1">
      <xsl:apply-templates select="center/table/tr/table/tr" />
    </xsl:if>
  </xsl:template>
  <xsl:template match="center/table/tr/table/tr">
    <xsl:if test="position() > 1">
      <listing>
        <xsl:apply-templates select="td" />
      </listing>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="td">
    <xsl:choose>
      <xsl:when test="position() = 1">
        <name>
          <xsl:apply-templates select="a" />
        </name>
      </xsl:when>
      <xsl:when test="position() = 2">
        <bids>
          <xsl:value-of select="current()"/>
        </bids>
      </xsl:when>
      <xsl:when test="position() = 3">
        <avgbid>
          <xsl:value-of select="current()"/>
        </avgbid>
      </xsl:when>
      <xsl:when test="position() = 4">
        <type>
          <xsl:value-of select="current()"/>
        </type>
      </xsl:when>
      <xsl:when test="position() = 5">
        <started>
          <xsl:value-of select="current()"/>
        </started>
      </xsl:when>
      <xsl:when test="position() = 6">
        <ends>
          <xsl:value-of select="current()"/>
        </ends>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="a">
    <link>
      <xsl:value-of select="@href" />
    </link>
    <text>
      <xsl:value-of select="/" />
    </text>
  </xsl:template>

</xsl:stylesheet>
