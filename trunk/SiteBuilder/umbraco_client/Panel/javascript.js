function resizePanel(F,C,D,B){var A=jQuery(window).height();var E=jQuery(window).width();if(top.document.location==document.location){resizePanelTo(F,C,E-12,A-20,B)}else{resizePanelTo(F,C,E,A,B)}if(D){jQuery(window).wresize(function(){resizePanel(F,C,false,"resize event")})}}function resizePanelTo(H,D,A,E,C){var B=jQuery("#"+H);var G=jQuery("#"+H+"_content");panelWidth=A;contentHeight=E-46;if(D){contentHeight=contentHeight-34}B.width(panelWidth);if(E>0){B.height(E)}if(G!=null){if(panelWidth>0){G.width(panelWidth-6)}if(contentHeight>0){G.height(contentHeight)}}if(D&&panelWidth>0){var F=panelWidth-35;jQuery("#"+H+"_menu").width(F);jQuery("#"+H+"_menu_slh").width(F);jQuery("#"+H+"_menubackground").width(panelWidth-2)}};