(function(){var D=tinymce.DOM,B=tinymce.dom.Event,G=tinymce.extend,E=tinymce.each,A=tinymce.util.Cookie,F,C=tinymce.explode;tinymce.ThemeManager.requireLangPack("advanced");tinymce.create("tinymce.themes.AdvancedTheme",{sizes:[8,10,12,14,18,24,36],controls:{bold:["bold_desc","Bold"],italic:["italic_desc","Italic"],underline:["underline_desc","Underline"],strikethrough:["striketrough_desc","Strikethrough"],justifyleft:["justifyleft_desc","JustifyLeft"],justifycenter:["justifycenter_desc","JustifyCenter"],justifyright:["justifyright_desc","JustifyRight"],justifyfull:["justifyfull_desc","JustifyFull"],bullist:["bullist_desc","InsertUnorderedList"],numlist:["numlist_desc","InsertOrderedList"],outdent:["outdent_desc","Outdent"],indent:["indent_desc","Indent"],cut:["cut_desc","Cut"],copy:["copy_desc","Copy"],paste:["paste_desc","Paste"],undo:["undo_desc","Undo"],redo:["redo_desc","Redo"],link:["link_desc","mceLink"],unlink:["unlink_desc","unlink"],image:["image_desc","mceImage"],cleanup:["cleanup_desc","mceCleanup"],help:["help_desc","mceHelp"],code:["code_desc","mceCodeEditor"],hr:["hr_desc","InsertHorizontalRule"],removeformat:["removeformat_desc","RemoveFormat"],sub:["sub_desc","subscript"],sup:["sup_desc","superscript"],forecolor:["forecolor_desc","ForeColor"],forecolorpicker:["forecolor_desc","mceForeColor"],backcolor:["backcolor_desc","HiliteColor"],backcolorpicker:["backcolor_desc","mceBackColor"],charmap:["charmap_desc","mceCharMap"],visualaid:["visualaid_desc","mceToggleVisualAid"],anchor:["anchor_desc","mceInsertAnchor"],newdocument:["newdocument_desc","mceNewDocument"],blockquote:["blockquote_desc","mceBlockQuote"]},stateControls:["bold","italic","underline","strikethrough","bullist","numlist","justifyleft","justifycenter","justifyright","justifyfull","sub","sup","blockquote"],init:function(I,J){var K=this,L,H,M;K.editor=I;K.url=J;K.onResolveName=new tinymce.util.Dispatcher(this);K.settings=L=G({theme_advanced_path:true,theme_advanced_toolbar_location:"bottom",theme_advanced_buttons1:"bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect",theme_advanced_buttons2:"bullist,numlist,|,outdent,indent,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code",theme_advanced_buttons3:"hr,removeformat,visualaid,|,sub,sup,|,charmap",theme_advanced_blockformats:"p,address,pre,h1,h2,h3,h4,h5,h6",theme_advanced_toolbar_align:"center",theme_advanced_fonts:"Andale Mono=andale mono,times;Arial=arial,helvetica,sans-serif;Arial Black=arial black,avant garde;Book Antiqua=book antiqua,palatino;Comic Sans MS=comic sans ms,sans-serif;Courier New=courier new,courier;Georgia=georgia,palatino;Helvetica=helvetica;Impact=impact,chicago;Symbol=symbol;Tahoma=tahoma,arial,helvetica,sans-serif;Terminal=terminal,monaco;Times New Roman=times new roman,times;Trebuchet MS=trebuchet ms,geneva;Verdana=verdana,geneva;Webdings=webdings;Wingdings=wingdings,zapf dingbats",theme_advanced_more_colors:1,theme_advanced_row_height:23,theme_advanced_resize_horizontal:1,theme_advanced_resizing_use_cookie:1,theme_advanced_font_sizes:"1,2,3,4,5,6,7",readonly:I.settings.readonly},I.settings);if(!L.font_size_style_values){L.font_size_style_values="8pt,10pt,12pt,14pt,18pt,24pt,36pt"}if(tinymce.is(L.theme_advanced_font_sizes,"string")){L.font_size_style_values=tinymce.explode(L.font_size_style_values);L.font_size_classes=tinymce.explode(L.font_size_classes||"");M={};I.settings.theme_advanced_font_sizes=L.theme_advanced_font_sizes;E(I.getParam("theme_advanced_font_sizes","","hash"),function(P,O){var N;if(O==P&&P>=1&&P<=7){O=P+" ("+K.sizes[P-1]+"pt)";if(I.settings.convert_fonts_to_spans){N=L.font_size_classes[P-1];P=L.font_size_style_values[P-1]||(K.sizes[P-1]+"pt")}}if(/\s*\./.test(P)){N=P.replace(/\./g,"")}M[O]=N?{"class":N}:{fontSize:P}});L.theme_advanced_font_sizes=M}if((H=L.theme_advanced_path_location)&&H!="none"){L.theme_advanced_statusbar_location=L.theme_advanced_path_location}if(L.theme_advanced_statusbar_location=="none"){L.theme_advanced_statusbar_location=0}I.onInit.add(function(){I.onNodeChange.add(K._nodeChanged,K);if(I.settings.content_css!==false){I.dom.loadCSS(I.baseURI.toAbsolute("themes/advanced/skins/"+I.settings.skin+"/content.css"))}});I.onSetProgressState.add(function(P,N,Q){var R,S=P.id,O;if(N){K.progressTimer=setTimeout(function(){R=P.getContainer();R=R.insertBefore(D.create("DIV",{style:"position:relative"}),R.firstChild);O=D.get(P.id+"_tbl");D.add(R,"div",{id:S+"_blocker","class":"mceBlocker",style:{width:O.clientWidth+2,height:O.clientHeight+2}});D.add(R,"div",{id:S+"_progress","class":"mceProgress",style:{left:O.clientWidth/2,top:O.clientHeight/2}})},Q||0)}else{D.remove(S+"_blocker");D.remove(S+"_progress");clearTimeout(K.progressTimer)}});D.loadCSS(L.editor_css?I.documentBaseURI.toAbsolute(L.editor_css):J+"/skins/"+I.settings.skin+"/ui.css");if(L.skin_variant){D.loadCSS(J+"/skins/"+I.settings.skin+"/ui_"+L.skin_variant+".css")}},createControl:function(K,H){var I,J;if(J=H.createControl(K)){return J}switch(K){case"styleselect":return this._createStyleSelect();case"formatselect":return this._createBlockFormats();case"fontselect":return this._createFontSelect();case"fontsizeselect":return this._createFontSizeSelect();case"forecolor":return this._createForeColorMenu();case"backcolor":return this._createBackColorMenu()}if((I=this.controls[K])){return H.createButton(K,{title:"advanced."+I[0],cmd:I[1],ui:I[2],value:I[3]})}},execCommand:function(J,I,K){var H=this["_"+J];if(H){H.call(this,I,K);return true}return false},_importClasses:function(I){var H=this.editor,J=H.controlManager.get("styleselect");if(J.getLength()==0){E(H.dom.getClasses(),function(K){J.add(K["class"],K["class"])})}},_createStyleSelect:function(L){var I=this,H=I.editor,J=H.controlManager,K=J.createListBox("styleselect",{title:"advanced.style_select",onselect:function(M){if(K.selectedValue===M){H.execCommand("mceSetStyleInfo",0,{command:"removeformat"});K.select();return false}else{H.execCommand("mceSetCSSClass",0,M)}}});if(K){E(H.getParam("theme_advanced_styles","","hash"),function(N,M){if(N){K.add(I.editor.translate(M),N)}});K.onPostRender.add(function(M,N){if(!K.NativeListBox){B.add(N.id+"_text","focus",I._importClasses,I);B.add(N.id+"_text","mousedown",I._importClasses,I);B.add(N.id+"_open","focus",I._importClasses,I);B.add(N.id+"_open","mousedown",I._importClasses,I)}else{B.add(N.id,"focus",I._importClasses,I)}})}return K},_createFontSelect:function(){var J,I=this,H=I.editor;J=H.controlManager.createListBox("fontselect",{title:"advanced.fontdefault",cmd:"FontName"});if(J){E(H.getParam("theme_advanced_fonts",I.settings.theme_advanced_fonts,"hash"),function(L,K){J.add(H.translate(K),L,{style:L.indexOf("dings")==-1?"font-family:"+L:""})})}return J},_createFontSizeSelect:function(){var K=this,I=K.editor,L,J=0,H=[];L=I.controlManager.createListBox("fontsizeselect",{title:"advanced.font_size",onselect:function(M){if(M.fontSize){I.execCommand("FontSize",false,M.fontSize)}else{E(K.settings.theme_advanced_font_sizes,function(O,N){if(O["class"]){H.push(O["class"])}});I.editorCommands._applyInlineStyle("span",{"class":M["class"]},{check_classes:H})}}});if(L){E(K.settings.theme_advanced_font_sizes,function(N,M){var O=N.fontSize;if(O>=1&&O<=7){O=K.sizes[parseInt(O)-1]+"pt"}L.add(M,N,{style:"font-size:"+O,"class":"mceFontSize"+(J++)+(" "+(N["class"]||""))})})}return L},_createBlockFormats:function(){var J,H={p:"advanced.paragraph",address:"advanced.address",pre:"advanced.pre",h1:"advanced.h1",h2:"advanced.h2",h3:"advanced.h3",h4:"advanced.h4",h5:"advanced.h5",h6:"advanced.h6",div:"advanced.div",blockquote:"advanced.blockquote",code:"advanced.code",dt:"advanced.dt",dd:"advanced.dd",samp:"advanced.samp"},I=this;J=I.editor.controlManager.createListBox("formatselect",{title:"advanced.block",cmd:"FormatBlock"});if(J){E(I.editor.getParam("theme_advanced_blockformats",I.settings.theme_advanced_blockformats,"hash"),function(L,K){J.add(I.editor.translate(K!=L?K:H[L]),L,{"class":"mce_formatPreview mce_"+L})})}return J},_createForeColorMenu:function(){var L,I=this,J=I.settings,K={},H;if(J.theme_advanced_more_colors){K.more_colors_func=function(){I._mceColorPicker(0,{color:L.value,func:function(M){L.setColor(M)}})}}if(H=J.theme_advanced_text_colors){K.colors=H}K.title="advanced.forecolor_desc";K.cmd="ForeColor";K.scope=this;L=I.editor.controlManager.createColorSplitButton("forecolor",K);return L},_createBackColorMenu:function(){var L,I=this,J=I.settings,K={},H;if(J.theme_advanced_more_colors){K.more_colors_func=function(){I._mceColorPicker(0,{color:L.value,func:function(M){L.setColor(M)}})}}if(H=J.theme_advanced_background_colors){K.colors=H}K.title="advanced.backcolor_desc";K.cmd="HiliteColor";K.scope=this;L=I.editor.controlManager.createColorSplitButton("backcolor",K);return L},renderUI:function(J){var L,K,M,P=this,N=P.editor,Q=P.settings,O,I,H;L=I=D.create("span",{id:N.id+"_parent","class":"mceEditor "+N.settings.skin+"Skin"+(Q.skin_variant?" "+N.settings.skin+"Skin"+P._ufirst(Q.skin_variant):"")});if(!D.boxModel){L=D.add(L,"div",{"class":"mceOldBoxModel"})}L=O=D.add(L,"table",{id:N.id+"_tbl","class":"mceLayout",cellSpacing:0,cellPadding:0});L=M=D.add(L,"tbody");switch((Q.theme_advanced_layout_manager||"").toLowerCase()){case"rowlayout":K=P._rowLayout(Q,M,J);break;case"customlayout":K=N.execCallback("theme_advanced_custom_layout",Q,M,J,I);break;default:K=P._simpleLayout(Q,M,J,I)}L=J.targetNode;H=D.stdMode?O.getElementsByTagName("tr"):O.rows;D.addClass(H[0],"mceFirst");D.addClass(H[H.length-1],"mceLast");E(D.select("tr",M),function(R){D.addClass(R.firstChild,"mceFirst");D.addClass(R.childNodes[R.childNodes.length-1],"mceLast")});if(D.get(Q.theme_advanced_toolbar_container)){D.get(Q.theme_advanced_toolbar_container).appendChild(I)}else{D.insertAfter(I,L)}B.add(N.id+"_path_row","click",function(R){R=R.target;if(R.nodeName=="A"){P._sel(R.className.replace(/^.*mcePath_([0-9]+).*$/,"$1"));return B.cancel(R)}});if(!N.getParam("accessibility_focus")||N.getParam("tab_focus")){B.add(D.add(I,"a",{href:"#"},"<!-- IE -->"),"focus",function(){tinyMCE.get(N.id).focus()})}if(Q.theme_advanced_toolbar_location=="external"){J.deltaHeight=0}P.deltaHeight=J.deltaHeight;J.targetNode=null;return{iframeContainer:K,editorContainer:N.id+"_parent",sizeContainer:O,deltaHeight:J.deltaHeight}},getInfo:function(){return{longname:"Advanced theme",author:"Moxiecode Systems AB",authorurl:"http://tinymce.moxiecode.com",version:tinymce.majorVersion+"."+tinymce.minorVersion}},resizeBy:function(H,I){var J=D.get(this.editor.id+"_tbl");this.resizeTo(J.clientWidth+H,J.clientHeight+I)},resizeTo:function(H,K){var I=this.editor,J=I.settings,M=D.get(I.id+"_tbl"),N=D.get(I.id+"_ifr"),L;H=Math.max(J.theme_advanced_resizing_min_width||100,H);K=Math.max(J.theme_advanced_resizing_min_height||100,K);H=Math.min(J.theme_advanced_resizing_max_width||65535,H);K=Math.min(J.theme_advanced_resizing_max_height||65535,K);L=M.clientHeight-N.clientHeight;D.setStyle(N,"height",K-L);D.setStyles(M,{width:H,height:K})},destroy:function(){var H=this.editor.id;B.clear(H+"_resize");B.clear(H+"_path_row");B.clear(H+"_external_close")},_simpleLayout:function(S,N,J,H){var R=this,O=R.editor,P=S.theme_advanced_toolbar_location,L=S.theme_advanced_statusbar_location,K,I,M,Q;if(S.readonly){K=D.add(N,"tr");K=I=D.add(K,"td",{"class":"mceIframeContainer"});return I}if(P=="top"){R._addToolbars(N,J)}if(P=="external"){K=Q=D.create("div",{style:"position:relative"});K=D.add(K,"div",{id:O.id+"_external","class":"mceExternalToolbar"});D.add(K,"a",{id:O.id+"_external_close",href:"javascript:;","class":"mceExternalClose"});K=D.add(K,"table",{id:O.id+"_tblext",cellSpacing:0,cellPadding:0});M=D.add(K,"tbody");if(H.firstChild.className=="mceOldBoxModel"){H.firstChild.appendChild(Q)}else{H.insertBefore(Q,H.firstChild)}R._addToolbars(M,J);O.onMouseUp.add(function(){var U=D.get(O.id+"_external");D.show(U);D.hide(F);var T=B.add(O.id+"_external_close","click",function(){D.hide(O.id+"_external");B.remove(O.id+"_external_close","click",T)});D.show(U);D.setStyle(U,"top",0-D.getRect(O.id+"_tblext").h-1);D.hide(U);D.show(U);U.style.filter="";F=O.id+"_external";U=null})}if(L=="top"){R._addStatusBar(N,J)}if(!S.theme_advanced_toolbar_container){K=D.add(N,"tr");K=I=D.add(K,"td",{"class":"mceIframeContainer"})}if(P=="bottom"){R._addToolbars(N,J)}if(L=="bottom"){R._addStatusBar(N,J)}return I},_rowLayout:function(R,L,J){var Q=this,M=Q.editor,P,S,H=M.controlManager,K,I,O,N;P=R.theme_advanced_containers_default_class||"";S=R.theme_advanced_containers_default_align||"center";E(C(R.theme_advanced_containers||""),function(V,U){var T=R["theme_advanced_container_"+V]||"";switch(T.toLowerCase()){case"mceeditor":K=D.add(L,"tr");K=I=D.add(K,"td",{"class":"mceIframeContainer"});break;case"mceelementpath":Q._addStatusBar(L,J);break;default:N=(R["theme_advanced_container_"+V+"_align"]||S).toLowerCase();N="mce"+Q._ufirst(N);K=D.add(D.add(L,"tr"),"td",{"class":"mceToolbar "+(R["theme_advanced_container_"+V+"_class"]||P)+" "+N||S});O=H.createToolbar("toolbar"+U);Q._addControls(T,O);D.setHTML(K,O.renderHTML());J.deltaHeight-=R.theme_advanced_row_height}});return I},_addControls:function(I,H){var J=this,K=J.settings,L,M=J.editor.controlManager;if(K.theme_advanced_disable&&!J._disabled){L={};E(C(K.theme_advanced_disable),function(N){L[N]=1});J._disabled=L}else{L=J._disabled}E(C(I),function(O){var N;if(L&&L[O]){return}if(O=="tablecontrols"){E(["table","|","row_props","cell_props","|","row_before","row_after","delete_row","|","col_before","col_after","delete_col","|","split_cells","merge_cells"],function(P){P=J.createControl(P,M);if(P){H.add(P)}});return}N=J.createControl(O,M);if(N){H.add(N)}})},_addToolbars:function(P,I){var S=this,L,K,N=S.editor,T=S.settings,R,H=N.controlManager,O,J,M=[],Q;Q=T.theme_advanced_toolbar_align.toLowerCase();Q="mce"+S._ufirst(Q);J=D.add(D.add(P,"tr"),"td",{"class":"mceToolbar "+Q});if(!N.getParam("accessibility_focus")||N.getParam("tab_focus")){M.push(D.createHTML("a",{href:"#",onfocus:"tinyMCE.get('"+N.id+"').focus();"},"<!-- IE -->"))}M.push(D.createHTML("a",{href:"#",accesskey:"q",title:N.getLang("advanced.toolbar_focus")},"<!-- IE -->"));for(L=1;(R=T["theme_advanced_buttons"+L]);L++){K=H.createToolbar("toolbar"+L,{"class":"mceToolbarRow"+L});if(T["theme_advanced_buttons"+L+"_add"]){R+=","+T["theme_advanced_buttons"+L+"_add"]}if(T["theme_advanced_buttons"+L+"_add_before"]){R=T["theme_advanced_buttons"+L+"_add_before"]+","+R}S._addControls(R,K);M.push(K.renderHTML());I.deltaHeight-=T.theme_advanced_row_height}M.push(D.createHTML("a",{href:"#",accesskey:"z",title:N.getLang("advanced.toolbar_focus"),onfocus:"tinyMCE.getInstanceById('"+N.id+"').focus();"},"<!-- IE -->"));D.setHTML(J,M.join(""))},_addStatusBar:function(L,I){var J,P=this,M=P.editor,Q=P.settings,H,N,O,K;J=D.add(L,"tr");J=K=D.add(J,"td",{"class":"mceStatusbar"});J=D.add(J,"div",{id:M.id+"_path_row"},Q.theme_advanced_path?M.translate("advanced.path")+": ":"&nbsp;");D.add(J,"a",{href:"#",accesskey:"x"});if(Q.theme_advanced_resizing&&!tinymce.isOldWebKit){D.add(K,"a",{id:M.id+"_resize",href:"javascript:;",onclick:"return false;","class":"mceResize"});if(Q.theme_advanced_resizing_use_cookie){M.onPostRender.add(function(){var R=A.getHash("TinyMCE_"+M.id+"_size"),S=D.get(M.id+"_tbl");if(!R){return}if(Q.theme_advanced_resize_horizontal){S.style.width=Math.max(10,R.cw)+"px"}S.style.height=Math.max(10,R.ch)+"px";D.get(M.id+"_ifr").style.height=Math.max(10,parseInt(R.ch)+P.deltaHeight)+"px"})}M.onPostRender.add(function(){B.add(M.id+"_resize","mousedown",function(V){var X,U,R,T,W,S;X=D.get(M.id+"_tbl");R=X.clientWidth;T=X.clientHeight;miw=Q.theme_advanced_resizing_min_width||100;mih=Q.theme_advanced_resizing_min_height||100;maw=Q.theme_advanced_resizing_max_width||65535;mah=Q.theme_advanced_resizing_max_height||65535;U=D.add(D.get(M.id+"_parent"),"div",{"class":"mcePlaceHolder"});D.setStyles(U,{width:R,height:T});D.hide(X);D.show(U);H={x:V.screenX,y:V.screenY,w:R,h:T,dx:null,dy:null};N=B.add(D.doc,"mousemove",function(a){var Y,Z;H.dx=a.screenX-H.x;H.dy=a.screenY-H.y;Y=Math.max(miw,H.w+H.dx);Z=Math.max(mih,H.h+H.dy);Y=Math.min(maw,Y);Z=Math.min(mah,Z);if(Q.theme_advanced_resize_horizontal){U.style.width=Y+"px"}U.style.height=Z+"px";return B.cancel(a)});O=B.add(D.doc,"mouseup",function(Y){var Z;B.remove(D.doc,"mousemove",N);B.remove(D.doc,"mouseup",O);X.style.display="";D.remove(U);if(H.dx===null){return}Z=D.get(M.id+"_ifr");if(Q.theme_advanced_resize_horizontal){X.style.width=Math.max(10,H.w+H.dx)+"px"}X.style.height=Math.max(10,H.h+H.dy)+"px";Z.style.height=Math.max(10,Z.clientHeight+H.dy)+"px";if(Q.theme_advanced_resizing_use_cookie){A.setHash("TinyMCE_"+M.id+"_size",{cw:H.w+H.dx,ch:H.h+H.dy})}});return B.cancel(V)})})}I.deltaHeight-=21;J=L=null},_nodeChanged:function(K,P,J,N){var S=this,H,O=0,R,L,T=S.settings,Q,I,M;if(T.readonly){return}tinymce.each(S.stateControls,function(U){P.setActive(U,K.queryCommandState(S.controls[U][1]))});P.setActive("visualaid",K.hasVisual);P.setDisabled("undo",!K.undoManager.hasUndo()&&!K.typing);P.setDisabled("redo",!K.undoManager.hasRedo());P.setDisabled("outdent",!K.queryCommandState("Outdent"));H=D.getParent(J,"A");if(L=P.get("link")){if(!H||!H.name){L.setDisabled(!H&&N);L.setActive(!!H)}}if(L=P.get("unlink")){L.setDisabled(!H&&N);L.setActive(!!H&&!H.name)}if(L=P.get("anchor")){L.setActive(!!H&&H.name);if(tinymce.isWebKit){H=D.getParent(J,"IMG");L.setActive(!!H&&D.getAttrib(H,"mce_name")=="a")}}H=D.getParent(J,"IMG");if(L=P.get("image")){L.setActive(!!H&&J.className.indexOf("mceItem")==-1)}if(L=P.get("styleselect")){if(J.className){S._importClasses();L.select(J.className)}else{L.select()}}if(L=P.get("formatselect")){H=D.getParent(J,D.isBlock);if(H){L.select(H.nodeName.toLowerCase())}}if(K.settings.convert_fonts_to_spans){K.dom.getParent(J,function(U){if(U.nodeName==="SPAN"){if(!Q&&U.className){Q=U.className}if(!I&&U.style.fontSize){I=U.style.fontSize}if(!M&&U.style.fontFamily){M=U.style.fontFamily.replace(/[\"\']+/g,"").replace(/^([^,]+).*/,"$1").toLowerCase()}}return false});if(L=P.get("fontselect")){L.select(function(U){return U.replace(/^([^,]+).*/,"$1").toLowerCase()==M})}if(L=P.get("fontsizeselect")){L.select(function(U){if(U.fontSize&&U.fontSize===I){return true}if(U["class"]&&U["class"]===Q){return true}})}}else{if(L=P.get("fontselect")){L.select(K.queryCommandValue("FontName"))}if(L=P.get("fontsizeselect")){R=K.queryCommandValue("FontSize");L.select(function(U){return U.fontSize==R})}}if(T.theme_advanced_path&&T.theme_advanced_statusbar_location){H=D.get(K.id+"_path")||D.add(K.id+"_path_row","span",{id:K.id+"_path"});D.setHTML(H,"");K.dom.getParent(J,function(Y){var U=Y.nodeName.toLowerCase(),V,X,W="";if(Y.nodeType!=1||Y.nodeName==="BR"||(D.hasClass(Y,"mceItemHidden")||D.hasClass(Y,"mceItemRemoved"))){return}if(R=D.getAttrib(Y,"mce_name")){U=R}if(tinymce.isIE&&Y.scopeName!=="HTML"){U=Y.scopeName+":"+U}U=U.replace(/mce\:/g,"");switch(U){case"b":U="strong";break;case"i":U="em";break;case"img":if(R=D.getAttrib(Y,"src")){W+="src: "+R+" "}break;case"a":if(R=D.getAttrib(Y,"name")){W+="name: "+R+" ";U+="#"+R}if(R=D.getAttrib(Y,"href")){W+="href: "+R+" "}break;case"font":if(T.convert_fonts_to_spans){U="span"}if(R=D.getAttrib(Y,"face")){W+="font: "+R+" "}if(R=D.getAttrib(Y,"size")){W+="size: "+R+" "}if(R=D.getAttrib(Y,"color")){W+="color: "+R+" "}break;case"span":if(R=D.getAttrib(Y,"style")){W+="style: "+R+" "}break}if(R=D.getAttrib(Y,"id")){W+="id: "+R+" "}if(R=Y.className){R=R.replace(/(webkit-[\w\-]+|Apple-[\w\-]+|mceItem\w+|mceVisualAid)/g,"");if(R&&R.indexOf("mceItem")==-1){W+="class: "+R+" ";if(D.isBlock(Y)||U=="img"||U=="span"){U+="."+R}}}U=U.replace(/(html:)/g,"");U={name:U,node:Y,title:W};S.onResolveName.dispatch(S,U);W=U.title;U=U.name;X=D.create("a",{href:"javascript:;",onmousedown:"return false;",title:W,"class":"mcePath_"+(O++)},U);if(H.hasChildNodes()){H.insertBefore(D.doc.createTextNode(" \u00bb "),H.firstChild);H.insertBefore(X,H.firstChild)}else{H.appendChild(X)}},K.getBody())}},_sel:function(H){this.editor.execCommand("mceSelectNodeDepth",false,H)},_mceInsertAnchor:function(J,I){var H=this.editor;H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/anchor.htm",width:320+parseInt(H.getLang("advanced.anchor_delta_width",0)),height:90+parseInt(H.getLang("advanced.anchor_delta_height",0)),inline:true},{theme_url:this.url})},_mceCharMap:function(){var H=this.editor;H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/charmap.htm",width:550+parseInt(H.getLang("advanced.charmap_delta_width",0)),height:250+parseInt(H.getLang("advanced.charmap_delta_height",0)),inline:true},{theme_url:this.url})},_mceHelp:function(){var H=this.editor;H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/about.htm",width:480,height:380,inline:true},{theme_url:this.url})},_mceColorPicker:function(J,I){var H=this.editor;I=I||{};H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/color_picker.htm",width:375+parseInt(H.getLang("advanced.colorpicker_delta_width",0)),height:250+parseInt(H.getLang("advanced.colorpicker_delta_height",0)),close_previous:false,inline:true},{input_color:I.color,func:I.func,theme_url:this.url})},_mceCodeEditor:function(I,J){var H=this.editor;H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/source_editor.htm",width:parseInt(H.getParam("theme_advanced_source_editor_width",720)),height:parseInt(H.getParam("theme_advanced_source_editor_height",580)),inline:true,resizable:true,maximizable:true},{theme_url:this.url})},_mceImage:function(I,J){var H=this.editor;if(H.dom.getAttrib(H.selection.getNode(),"class").indexOf("mceItem")!=-1){return}H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/image.htm",width:355+parseInt(H.getLang("advanced.image_delta_width",0)),height:275+parseInt(H.getLang("advanced.image_delta_height",0)),inline:true},{theme_url:this.url})},_mceLink:function(I,J){var H=this.editor;H.windowManager.open({url:tinymce.baseURL+"/themes/advanced/link.htm",width:310+parseInt(H.getLang("advanced.link_delta_width",0)),height:200+parseInt(H.getLang("advanced.link_delta_height",0)),inline:true},{theme_url:this.url})},_mceNewDocument:function(){var H=this.editor;H.windowManager.confirm("advanced.newdocument",function(I){if(I){H.execCommand("mceSetContent",false,"")}})},_mceForeColor:function(){var H=this;this._mceColorPicker(0,{color:H.fgColor,func:function(I){H.fgColor=I;H.editor.execCommand("ForeColor",false,I)}})},_mceBackColor:function(){var H=this;this._mceColorPicker(0,{color:H.bgColor,func:function(I){H.bgColor=I;H.editor.execCommand("HiliteColor",false,I)}})},_ufirst:function(H){return H.substring(0,1).toUpperCase()+H.substring(1)}});tinymce.ThemeManager.add("advanced",tinymce.themes.AdvancedTheme)}());