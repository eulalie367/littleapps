<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="table">
    <xsl:apply-templates select="tr" />
  </xsl:template>

  <xsl:template match="tr">
    <xsl:if test="position() != 1">
      <word>
        <xsl:apply-templates select="td" />
      </word>
    </xsl:if>
  </xsl:template>

  <xsl:template match="td">
    <xsl:choose>
      <xsl:when test="position() mod 3 = 1">
        <rank>
          <xsl:value-of select="current()"/>
        </rank>
      </xsl:when>
      <xsl:when test="position() mod 3 = 2">
        <xsl:apply-templates select="a" />
      </xsl:when>
      <xsl:when test="position() mod 3 = 0">
        <uses>
          <xsl:value-of select="current()"/>
        </uses>
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="a">
    <link>
      http://en.wiktionary.org<xsl:value-of select="@href" />
    </link>
    <text>
      <xsl:value-of select="@title" />
    </text>
  </xsl:template>

</xsl:stylesheet>
