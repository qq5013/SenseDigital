/////////////////////////////////////////////////////////////////////////////////////////
// Super Tables v0.30 - MIT Style License
// Copyright (c) 2008 Matt Murphy --- www.matts411.com
//
// Contributors:
// Joe Gallo
/////////////////////////////////////////////////////////////////////////////////////////
////// TO CALL: 
// new superTable([string] tableId, [object] options);
//
////// OPTIONS: (order does not matter )
// cssSkin : string ( eg. "sDefault", "sSky", "sOrange", "sDark" )
// headerRows : integer ( default is 1 )
// fixedCols : integer ( default is 0 )
// colWidths : integer array ( use -1 for auto sizing )
// onStart : function ( any this.variableNameHere variables you create here can be used later ( eg. onFinish function ) )
// onFinish : function ( all this.variableNameHere variables created in this script can be used in this function )
//
////// EXAMPLES:
// var myST = new superTable("myTableId");
//
// var myST = new superTable("myTableId", {
//		cssSkin : "sDefault",
//		headerRows : 1,
//		fixedCols : 2,
//		colWidths : [100, 230, 220, -1, 120, -1, -1, 120],
//		onStart : function () {
//			this.start = new Date();
//		},
//		onFinish : function () {
//			alert("Finished... " + ((new Date()) - this.start) + "ms.");
//		}
// });
//
////// ISSUES / NOTES:
// 1. No quirksmode support (officially, but still should work)
// 2. Element id's may be duplicated when fixedCols > 0, causing getElementById() issues
// 3. Safari will render the header row incorrectly if the fixed header row count is 1 and there is a colspan > 1 in one 
//		or more of the cells (fix available)
/////////////////////////////////////////////////////////////////////////////////////////

var superTable = function(tableId, options) {
    /////* Initialize */
    options = options || {};
    this.cssSkin = options.cssSkin || "";
    this.headerRows = parseInt(options.headerRows || "1");
    this.fixedCols = parseInt(options.fixedCols || "0");
    this.colWidths = options.colWidths || [];
    this.colNames = options.colNames || [];
    this.initFunc = options.onStart || null;
    this.callbackFunc = options.onFinish || null;
    this.initFunc && this.initFunc();

    /////* Create the framework dom */
    this.sBase = document.createElement("DIV");
    this.sFHeader = this.sBase.cloneNode(false);
    this.sHeader = this.sBase.cloneNode(false);
    this.sHeaderInner = this.sBase.cloneNode(false);
    this.sFData = this.sBase.cloneNode(false);
    this.sFDataInner = this.sBase.cloneNode(false);
    this.sData = this.sBase.cloneNode(false);
    this.sColGroup = document.createElement("COLGROUP");
    this.sFColGroup = document.createElement("COLGROUP");
    this.sHColGroup = document.createElement("COLGROUP");
    this.sFHColGroup = document.createElement("COLGROUP");

    this.sDataTable = document.getElementById(tableId);
    this.sDataTable.style.margin = "0px"; /* Otherwise looks bad */
    this.sDataTable.style.padding = "0px";
    
    if (this.cssSkin !== "") {
        this.sDataTable.className += " " + this.cssSkin;
    }
    if (this.sDataTable.getElementsByTagName("COLGROUP").length > 0) {
        this.sDataTable.removeChild(this.sDataTable.getElementsByTagName("COLGROUP")[0]); /* Making our own */
    }
    this.sParent = this.sDataTable.parentNode;
    //    this.sParentHeight = this.sParent.offsetHeight;
    //    this.sParentWidth = this.sParent.offsetWidth;
    var blnPx = 2;
    if (this.sParent.style.width.indexOf('%') > 0) //當設置width為%時，微調18px;
        blnPx = 2;

    this.sParentHeight = this.sParent.style.pixelHeight;
    //this.sParentWidth = this.sParent.style.pixelWidth;
    var tbIndex = tableId.substring(tableId.length - 1) - 1;
    this.sParentWidth = divWidth[tbIndex];

    /////* Attach the required classNames */
    this.sBase.className = "sBase";
    this.sFHeader.className = "sFHeader";
    this.sHeader.className = "sHeader";
    this.sHeaderInner.className = "sHeaderInner";
    this.sFData.className = "sFData";
    this.sFDataInner.className = "sFDataInner";
    this.sData.className = "sData";

    this.sBase.id = tableId + "_sBase";
    this.sFHeader.id = tableId + "_sFHeader";
    this.sHeader.id = tableId + "_sHeader";
    this.sHeaderInner.id = tableId + "_sHeaderInner";
    this.sFData.id = tableId + "_sFData";
    this.sFDataInner.id = tableId + "_sFDataInner";
    this.sData.id = tableId + "_sData";


    /////* Clone parts of the data table for the new header table */
    var alpha, beta, touched, clean, cleanRow, i, j, k, m, n, p;
    this.sHeaderTable = this.sDataTable.cloneNode(false);
    if (this.sDataTable.tHead) {
        alpha = this.sDataTable.tHead;
        this.sHeaderTable.appendChild(alpha.cloneNode(false));
        beta = this.sHeaderTable.tHead;
    } else {
        alpha = this.sDataTable.tBodies[0];
        this.sHeaderTable.appendChild(alpha.cloneNode(false));
        beta = this.sHeaderTable.tBodies[0];
    }
    alpha = alpha.rows;
    for (i = 0; i < this.headerRows; i++) {
        beta.appendChild(alpha[i].cloneNode(true));
    }
    this.sHeaderInner.appendChild(this.sHeaderTable);


    this.sFHeaderTable = this.sHeaderTable.cloneNode(true);
    this.sFHeader.appendChild(this.sFHeaderTable);
    this.sFDataTable = this.sDataTable.cloneNode(true);
    this.sFDataInner.appendChild(this.sFDataTable);

    this.sFHeaderTable.id = tableId + "_sFHeaderTable";
    this.sHeaderTable.id = tableId + "_sHeaderTable";
    this.sFDataTable.id = tableId + "_sFDataTable";
    this.sDataTable.id = tableId + "_sDataTable";



    /////* Set up the colGroup */
    alpha = this.sDataTable.tBodies[0].rows;
    for (i = 0, j = alpha.length; i < j; i++) {
        clean = true;
        for (k = 0, m = alpha[i].cells.length; k < m; k++) {
            if (alpha[i].cells[k].colSpan !== 1 || alpha[i].cells[k].rowSpan !== 1) {
                i += alpha[i].cells[k].rowSpan - 1;
                clean = false;
                break;
            }
        }
        if (clean === true) break; /* A row with no cells of colSpan > 1 || rowSpan > 1 has been found */
    }
    cleanRow = (clean === true) ? i : 0; /* Use this row index to calculate the column widths */
    //    for (i = 0, j = alpha[cleanRow].cells.length; i < j; i++) {
    //        if (i === this.colWidths.length || this.colWidths[i] === -1) {
    //            this.colWidths[i] = alpha[cleanRow].cells[i].offsetWidth;
    //        }
    //    }
    //    for (i = 0, j = this.colWidths.length; i < j; i++) {
    //        this.sColGroup.appendChild(document.createElement("COL"));
    //        this.sColGroup.lastChild.setAttribute("width", this.colWidths[i]);
    //        this.sColGroup.lastChild.setAttribute("id", this.colNames[i]);

    //        this.sFColGroup.appendChild(document.createElement("COL"));
    //        this.sFColGroup.lastChild.setAttribute("width", this.colWidths[i]);
    //        this.sFColGroup.lastChild.setAttribute("id", "F" + this.colNames[i]);

    //        this.sHColGroup.appendChild(document.createElement("COL"));
    //        this.sHColGroup.lastChild.setAttribute("width", this.colWidths[i]);
    //        this.sHColGroup.lastChild.setAttribute("id", "H" + this.colNames[i]);

    //        this.sFHColGroup.appendChild(document.createElement("COL"));
    //        this.sFHColGroup.lastChild.setAttribute("width", this.colWidths[i]);
    //        this.sFHColGroup.lastChild.setAttribute("id", "FH" + this.colNames[i]);
    //    }

    this.sDataTable.insertBefore(this.sColGroup.cloneNode(true), this.sDataTable.firstChild);
    this.sHeaderTable.insertBefore(this.sHColGroup.cloneNode(true), this.sHeaderTable.firstChild);
    if (this.fixedCols > 0) {
        this.sFHeaderTable.insertBefore(this.sFHColGroup.cloneNode(true), this.sFHeaderTable.firstChild);
        this.sFDataTable.insertBefore(this.sFColGroup.cloneNode(true), this.sFDataTable.firstChild);
    }

    /////* Style the tables individually if applicable */
    if (this.cssSkin !== "") {
        this.sDataTable.className += " " + this.cssSkin + "-Main";
        this.sHeaderTable.className += " " + this.cssSkin + "-Headers";
        if (this.fixedCols > 0) {
            this.sFDataTable.className += " " + this.cssSkin + "-Fixed";
            this.sFHeaderTable.className += " " + this.cssSkin + "-FixedHeaders";
        }
    }

    /////* Throw everything into sBase */
    if (this.fixedCols > 0) {
        this.sBase.appendChild(this.sFHeader);
    }
    this.sHeader.appendChild(this.sHeaderInner);
    this.sBase.appendChild(this.sHeader);
    if (this.fixedCols > 0) {
        this.sFData.appendChild(this.sFDataInner);
        this.sBase.appendChild(this.sFData);
    }
    this.sBase.appendChild(this.sData);
    this.sParent.insertBefore(this.sBase, this.sDataTable);
    this.sData.appendChild(this.sDataTable);

    /////* Align the tables */
    var sDataStyles, sDataTableStyles;
    if (this.sDataTable.tHead == null)
        this.sHeaderHeight = this.sDataTable.tBodies[0].rows[0].offsetTop;
    else
        this.sHeaderHeight = this.sDataTable.tBodies[0].rows[(this.sDataTable.tHead) ? 0 : this.headerRows].offsetTop;
    this.sHeaderHeight = 1.7;
    sDataTableStyles = "margin-top: " + (this.sHeaderHeight * -14) + "px;";
    sDataStyles = "margin-top: " + this.sHeaderHeight * 14 + "px;";
    //sDataTableStyles = "margin-top: " + 30 + "px;";
    //sDataStyles = "margin-top: " - 30 + "px;";
    sDataStyles += "height: " + (this.sParentHeight - this.sHeaderHeight - 23) + "px;";
    if (this.fixedCols > 0) {
        /* A collapsed table's cell's offsetLeft is calculated differently (w/ or w/out border included) across broswers - adjust: */
        var d = 0;
        for (var ddd = 0; ddd < this.fixedCols; ddd++)
            d += parseInt(this.colWidths[ddd]);
        fixedColsWidth[tableId] = d;
        this.sFHeaderWidth = d + 0.5;

        //        this.sFHeaderWidth = this.sDataTable.tBodies[0].rows[cleanRow].cells[this.fixedCols].offsetLeft;
        if (window.getComputedStyle) {
            alpha = document.defaultView;
            beta = this.sDataTable.tBodies[0].rows[0].cells[0];
            if (navigator.taintEnabled) { /* If not Safari */
                this.sFHeaderWidth += Math.ceil(parseInt(alpha.getComputedStyle(beta, null).getPropertyValue("border-right-width")) / 2);
            } else {
                this.sFHeaderWidth += parseInt(alpha.getComputedStyle(beta, null).getPropertyValue("border-right-width"));
            }
        } else if (/*@cc_on!@*/0) { /* Internet Explorer */
            alpha = this.sDataTable.tBodies[0].rows[0].cells[0];
            beta = [alpha.currentStyle["borderRightWidth"], alpha.currentStyle["borderLeftWidth"]];
            if (/px/i.test(beta[0]) && /px/i.test(beta[1])) {
                beta = [parseInt(beta[0]), parseInt(beta[1])].sort();
                this.sFHeaderWidth += Math.ceil(parseInt(beta[1]) / 2);
            }
        }

        /* Opera 9.5 issue - a sizeable data table may cause the document scrollbars to appear without this: */
        if (window.opera) {
            this.sFData.style.height = this.sParentHeight + "px";
        }

        this.sFHeader.style.width = this.sFHeaderWidth + "px";
        sDataTableStyles += "margin-left: " + (this.sFHeaderWidth * -1) + "px;";
        sDataStyles += "margin-left: " + this.sFHeaderWidth + "px;";
        sDataStyles += "width: " + (this.sParentWidth - this.sFHeaderWidth - blnPx + 2) + "px;";
    } else {
        sDataStyles += "width: " + this.sParentWidth - blnPx + "px;";
    }
    this.sData.style.cssText = sDataStyles;
    this.sDataTable.style.cssText = sDataTableStyles;

    /////* Set up table scrolling and IE's onunload event for garbage collection */
    (function(st) {
        if (st.fixedCols > 0) {
            st.sData.onscroll = function() {
                st.sHeaderInner.style.right = st.sData.scrollLeft + "px";
                st.sFDataInner.style.top = (st.sData.scrollTop * -1) + "px";
            };
        } else {
            st.sData.onscroll = function() {
                st.sHeaderInner.style.right = st.sData.scrollLeft + "px";
            };
        }
        if (/*@cc_on!@*/0) { /* Internet Explorer */
            window.attachEvent("onunload", function() {
                st.sData.onscroll = null;
                st = null;
            });
        }
    })(this);

    this.callbackFunc && this.callbackFunc();
    //Edit SuperTable To Be Resizable//////////////////////////////////
    // true when a header is currently being resized
    var _isResizing;
    // a reference to the header column that is being resized
    var _element;
    // an array of all of the tables header cells
    var _ths;
    // _ths = $get('sHeadertable').getElementsByTagName('colgroup');
    _ths = this.sHeaderTable.getElementsByTagName('th');

    // if the grid has at least one th element
    if (_ths.length > 1) {
        for (i = 0; i < _ths.length; i++) {
            _ths[i].setAttribute("id", i);
            // determine the widths
            //   _ths[i].style.width = Sys.UI.DomElement.getBounds(_ths[i]).width + 'px';
            // attach the mousemove and mousedown events
            if (i >= this.fixedCols) {
                $addHandler(_ths[i], 'mousemove', _onMouseMove);
                $addHandler(_ths[i], 'mousedown', _onMouseDown);
            }
        }
        // add a global mouseup handler
        //$addHandler(document, 'mousemove', _onMouseMove);
        $addHandler(document, 'mouseup', _onMouseUp);
        // add a global selectstart handler
        $addHandler(document, 'selectstart', _onSelectStart);

    }

    function _onMouseMove(args) {
        if (_isResizing) {
            // determine the new width of the header

            var bounds = Sys.UI.DomElement.getBounds(_element);
            var width = args.clientX - bounds.x;

            // we set the minimum width to 1 px, so make
            // sure it is at least this before bothering to
            // calculate the new width
            if (width > 10) {
                // get the next th element so we can adjust its size as well
                //     var nextColumn = _element.nextSibling;
                //     var nextColumnWidth;
                //     if(width < _toNumber(_element.style.width)){
                //      // make the next column bigger
                //      nextColumnWidth = _toNumber(nextColumn.style.width) + _toNumber(_element.style.width) - width;
                //     }
                //     else if(width > _toNumber(_element.style.width)){
                //      // make the next column smaller
                //      nextColumnWidth = _toNumber(nextColumn.style.width) - (width - _toNumber(_element.style.width));
                //     } 
                //     
                //     // we also don't want to shrink this width to less than one pixel,
                //     // so make sure of this before resizing ...
                //     if(nextColumnWidth > 1){
                //      _element.style.width = width + 'px';
                //      nextColumn.style.width = nextColumnWidth + 'px';
                //     }
                //    _element.style.width = width + 'px';
                var thID = _element.id;

                $get(tableId + "_sHeaderTable").getElementsByTagName('th')[thID].style.width = width + 'px';
                $get(tableId + "_sFHeaderTable").getElementsByTagName('th')[thID].style.width = width + 'px';
                $get(tableId + "_sDataTable").getElementsByTagName('th')[thID].style.width = width + 'px';
                $get(tableId + "_sFDataTable").getElementsByTagName('th')[thID].style.width = width + 'px';
                //調整列寬時
                $("DiV[class='sData']").scrollLeft($("DiV[class='sData']").scrollLeft() - 1);
                $("DiV[class='sData']").scrollLeft($("DiV[class='sData']").scrollLeft() + 1);

            }
        }
        else {
            // get the bounds of the element. If the mouse cursor is within
            // 2px of the border, display the e-cursor -> cursor:e-resize
            var bounds2 = Sys.UI.DomElement.getBounds(args.target);
            if (Math.abs((bounds2.x + bounds2.width) - (args.clientX)) <= 4) {
                //args.target.style.cursor = 'e-resize';
                args.target.style.cursor = 'col-resize';
            }
            else {
                args.target.style.cursor = '';
            }
        }
    }

    function _onMouseDown(args) {
        // if the user clicks the mouse button while
        // the cursor is in the resize position, it means
        // they want to start resizing. Set _isResizing to true
        // and grab the th element that is being resized
        if (args.target.style.cursor == 'col-resize') {
            _isResizing = true;
            _element = args.target;
        }
    }
    function _onMouseUp(args) {
        // the user let go of the mouse - so
        // they are done resizing the header. Reset
        // everything back
        if (_isResizing) {
            // set back to default values
            _isResizing = false;
            _element = null;
            // make sure the cursor is set back to default
            for (i = 0; i < _ths.length; i++) {
                _ths[i].style.cursor = '';
            }
        }
        //$("#"+tableId+"_sData")[0].scrollLeft
        //        if ($("#" + tableId + "_sData")[0].scrollLeft > 0)
        //            ;
        //        else
        //            $("#" + tableId + "_sData")[0].scrollLeft = scrollleft;
        //        //$("#"+tableId+"_sData")[0].scrollTop
        //        if ($("#" + tableId + "_sData")[0].scrollTop > 0)
        //            this.scrolltop = $("#" + tableId + "_sData")[0].scrollTop;
        //        else
        //            $("#" + tableId + "_sData")[0].scrollTop = this.scrolltop;
        //        if (args.target.id.replace(tableId, "") == "_sData") {
        //        
        //        }
    }

    function _onSelectStart(args) {
        // Don't allow selection during drag
        if (_isResizing) {
            args.preventDefault();
            return false;
        }
    }

    function _toNumber(m) {
        // helper function to peel the px off of the widths
        return new Number(m.replace('px', ''));
    }
    //Edit SuperTable To Be Resizable//////////////////////////////////
};