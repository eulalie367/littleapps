var TinyMCE_UmbracoLinkPlugin={getInfo:function(){return{longname:"Umbraco Link Dialog",author:"Christian Palm",authorurl:"http://www.umbraco.org",infourl:"http://www.umbraco.org",version:"0.9"}},initInstance:function(A){A.addShortcut("ctrl","t","lang_somename_desc","mceSomeCommand")},getControlHTML:function(A){switch(A){case"link":return tinyMCE.getButtonHTML(A,"lang_link_desc","{$themeurl}/images/link.gif","mceUmbracoLink")}return""},execCommand:function(G,D,C,A,H){switch(C){case"mceUmbracoLink":var F=false;var E=tinyMCE.getInstanceById(G);var J=E.getFocusElement();var B=E.selection.getSelectedText();if(tinyMCE.selectedElement){F=(tinyMCE.selectedElement.nodeName.toLowerCase()=="img")||(B&&B.length>0)}if(F||(J!=null&&J.nodeName=="A")){var I=new Array();I.file="../../plugins/umbracolink/link.htm";I.width=480;I.height=400;I.width+=tinyMCE.getLang("lang_advlink_delta_width",0);I.height+=tinyMCE.getLang("lang_advlink_delta_height",0);tinyMCE.openWindow(I,{editor_id:G,inline:"yes"})}return true}return false},handleNodeChange:function(F,D,E,C,A,B){if(D==null){return}do{if(D.nodeName=="A"&&tinyMCE.getAttrib(D,"href")!=""){tinyMCE.switchClass(F+"_advlink","mceButtonSelected");return true}}while((D=D.parentNode));if(B){tinyMCE.switchClass(F+"_advlink","mceButtonNormal");return true}tinyMCE.switchClass(F+"_advlink","mceButtonDisabled");return true},setupContent:function(C,A,B){},onChange:function(A){},handleEvent:function(A){return true},cleanup:function(A,B,C){return B},_someInternalFunction:function(B,A){return 1}};tinyMCE.addPlugin("umbracolink",TinyMCE_UmbracoLinkPlugin);