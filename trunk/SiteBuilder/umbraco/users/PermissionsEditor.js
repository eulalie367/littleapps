var PermissionsEditor = {
    /// <summary>
    /// Static class to perform the AJAX callback methods for the PermissionsEditor
    /// </summary>

    Init: function(_userID, permissionsContainerId, treeContainerId, replaceChildrenChkId) {
        /// <summary>
        /// Constructor method, must be called before the rest of the class can be used
        /// </summary>

        this._userID = parseInt(_userID);
        this._checkedPermissionsContainer = "#" + permissionsContainerId;
        this._treeContainer = "#" + treeContainerId;
        this._replaceChildChk = "#" + replaceChildrenChkId;
    },

    //private members
    _userID: -1,
    _loadingContent: '<div align="center"><br/><br/><br/><br/><br/><br/><br/><img src="/umbraco_client/images/progressBar.gif" /></div>',
    _checkedPermissionsContainer: "",
    _treeContainer: "",
    _selectedNodes: new Array(),
    _replaceChildChk: "",

    //public methods
    TreeNodeChecked: function(chk) {
        var vals = "";
        jQuery(this._treeContainer).find("input:checked").each(function() {
            //if the check box is not the one thats just been checked, add it
            if (jQuery(this).val() != jQuery(chk).val()) {
                vals += jQuery(this).val() + ",";
            }
        });
        this._selectedNodes = vals.split(",");
        this._selectedNodes.pop(); //remove the last one as it will be empty
        //add the one that was just checked to the end of the array if
        if (jQuery(chk).is(":checked")) {
            this._selectedNodes.push(jQuery(chk).val());
        }
        if (this._selectedNodes.length > 0) {
            this._beginShowNodePermissions(this._selectedNodes.join(","));
            jQuery(this._checkedPermissionsContainer).show();
        }
        else {
            jQuery(this._checkedPermissionsContainer).hide();
        }
    },
    SetReplaceChild: function(doReplace) {
        alert(doReplace);
        this._replaceChildren = doReplace;
    },
    BeginSavePermissions: function() {

        //ensure that there are nodes selected to save permissions against
        if (this._selectedNodes.length == 0) {
            alert("No nodes have been selected");
            return;
        }
        else if (!confirm("Permissions will be changed for nodes: " + this._selectedNodes.join(",") + ". Are you sure?")) {
            return;
        }

        //get the list of checked permissions
        var checkedPermissions = "";
        jQuery(this._checkedPermissionsContainer).find("input:checked").each(function() {
            checkedPermissions += jQuery(this).val();
        });

        var replaceChildren = jQuery(this._replaceChildChk).is(":checked");

        this._setUpdateProgress();
        setTimeout("PermissionsEditor._savePermissions('" + this._selectedNodes.join(",") + "','" + checkedPermissions + "'," + replaceChildren + ");", 10);
    },

    //private methods
    _addItemToSelectedCollection: function(chkbox) {
        if (chkbox.checked)
            this._selectedNodes.push(chkbox.value);
        else {
            var joined = this._selectedNodes.join(',');
            joined = joined.replace(chkbox.value, '');
            this._selectedNodes = joined.split(',');
            this._selectedNodes = ContentTreeControl_ReBuildArray(contentTreeControl_selected);
        }

    },
    _beginShowNodePermissions: function(selectedIDs) {
        this._setUpdateProgress();
        setTimeout("PermissionsEditor._showNodePermissions('" + selectedIDs + "');", 10);
    },
    _showNodePermissions: function(selectedIDs) {
        umbraco.cms.presentation.user.PermissionsHandler.GetNodePermissions(this._userID, selectedIDs, this._showNodePermissionsCallback);
    },
    _showNodePermissionsCallback: function(result) {
        jQuery(PermissionsEditor._checkedPermissionsContainer).html(result);
    },
    _savePermissions: function(nodeIDs, selectedPermissions, replaceChildren) {
        umbraco.cms.presentation.user.PermissionsHandler.SaveNodePermissions(this._userID, nodeIDs, selectedPermissions, replaceChildren, this._savePermissionsHandler);
    },
    _savePermissionsHandler: function(result) {
        if (top != null && top.UmbSpeechBubble != null)
            top.UmbSpeechBubble.ShowMessage("save", "Saved", "Permissions Saved");

        jQuery(PermissionsEditor._checkedPermissionsContainer).html(result);
    },
    _setUpdateProgress: function() {
        jQuery(this._checkedPermissionsContainer).html(this._loadingContent);
    }

}