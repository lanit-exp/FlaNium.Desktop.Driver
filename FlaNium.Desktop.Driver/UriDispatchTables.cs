namespace FlaNium.Desktop.Driver
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using FlaNium.Desktop.Driver.Common;

    #endregion

    internal class UriDispatchTables
    {
        #region Fields

        private readonly Dictionary<string, CommandInfo> commandDictionary = new Dictionary<string, CommandInfo>();

        private UriTemplateTable deleteDispatcherTable;

        private UriTemplateTable getDispatcherTable;

        private UriTemplateTable postDispatcherTable;

        #endregion

        #region Constructors and Destructors

        public UriDispatchTables(Uri prefix)
        {
            this.InitializeSeleniumCommandDictionary();
            this.InitializeFlaNiumCommandDictionary();
            this.ConstructDispatcherTables(prefix);
        }

        #endregion

        #region Public Methods and Operators

        public UriTemplateMatch Match(string httpMethod, Uri uriToMatch)
        {
            var table = this.FindDispatcherTable(httpMethod);
            return table != null ? table.MatchSingle(uriToMatch) : null;
        }

        #endregion

        #region Methods

        internal UriTemplateTable FindDispatcherTable(string httpMethod)
        {
            UriTemplateTable tableToReturn = null;
            switch (httpMethod)
            {
                case CommandInfo.GetCommand:
                    tableToReturn = this.getDispatcherTable;
                    break;

                case CommandInfo.PostCommand:
                    tableToReturn = this.postDispatcherTable;
                    break;

                case CommandInfo.DeleteCommand:
                    tableToReturn = this.deleteDispatcherTable;
                    break;
            }

            return tableToReturn;
        }

        private void ConstructDispatcherTables(Uri prefix)
        {
            this.getDispatcherTable = new UriTemplateTable(prefix);
            this.postDispatcherTable = new UriTemplateTable(prefix);
            this.deleteDispatcherTable = new UriTemplateTable(prefix);

            var fields = typeof(DriverCommand).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var field in fields)
            {
                var commandName = field.GetValue(null).ToString();
                var commandInformation = this.commandDictionary[commandName];
                var commandUriTemplate = new UriTemplate(commandInformation.ResourcePath);
                var templateTable = this.FindDispatcherTable(commandInformation.Method);
                templateTable.KeyValuePairs.Add(new KeyValuePair<UriTemplate, object>(commandUriTemplate, commandName));
            }

            this.getDispatcherTable.MakeReadOnly(false);
            this.postDispatcherTable.MakeReadOnly(false);
            this.deleteDispatcherTable.MakeReadOnly(false);
        }

        private void InitializeSeleniumCommandDictionary()
        {
            this.commandDictionary.Add(DriverCommand.DefineDriverMapping, new CommandInfo("POST", "/config/drivers"));
            this.commandDictionary.Add(DriverCommand.Status, new CommandInfo("GET", "/status"));
            this.commandDictionary.Add(DriverCommand.NewSession, new CommandInfo("POST", "/session"));
            this.commandDictionary.Add(DriverCommand.GetSessionList, new CommandInfo("GET", "/sessions"));
            this.commandDictionary.Add(
                DriverCommand.GetSessionCapabilities,
                new CommandInfo("GET", "/session/{sessionId}"));
            this.commandDictionary.Add(DriverCommand.Quit, new CommandInfo("DELETE", "/session/{sessionId}"));
            this.commandDictionary.Add(
                DriverCommand.GetCurrentWindowHandle,
                new CommandInfo("GET", "/session/{sessionId}/window_handle"));
            this.commandDictionary.Add(
                DriverCommand.GetWindowHandles,
                new CommandInfo("GET", "/session/{sessionId}/window_handles"));
            this.commandDictionary.Add(DriverCommand.GetCurrentUrl, new CommandInfo("GET", "/session/{sessionId}/url"));
            this.commandDictionary.Add(DriverCommand.Get, new CommandInfo("POST", "/session/{sessionId}/url"));
            this.commandDictionary.Add(DriverCommand.GoForward, new CommandInfo("POST", "/session/{sessionId}/forward"));
            this.commandDictionary.Add(DriverCommand.GoBack, new CommandInfo("POST", "/session/{sessionId}/back"));
            this.commandDictionary.Add(DriverCommand.Refresh, new CommandInfo("POST", "/session/{sessionId}/refresh"));
            this.commandDictionary.Add(
                DriverCommand.ExecuteScript,
                new CommandInfo("POST", "/session/{sessionId}/execute"));
            this.commandDictionary.Add(
                DriverCommand.ExecuteAsyncScript,
                new CommandInfo("POST", "/session/{sessionId}/execute_async"));
            this.commandDictionary.Add(
                DriverCommand.Screenshot,
                new CommandInfo("GET", "/session/{sessionId}/screenshot"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToParentFrame,
                new CommandInfo("POST", "/session/{sessionId}/frame/parent"));
            this.commandDictionary.Add(
                DriverCommand.SwitchToWindow,
                new CommandInfo("POST", "/session/{sessionId}/window"));
            this.commandDictionary.Add(
                DriverCommand.GetAllCookies,
                new CommandInfo("GET", "/session/{sessionId}/cookie"));
            this.commandDictionary.Add(DriverCommand.AddCookie, new CommandInfo("POST", "/session/{sessionId}/cookie"));
            this.commandDictionary.Add(
                DriverCommand.DeleteAllCookies,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie"));
            this.commandDictionary.Add(
                DriverCommand.DeleteCookie,
                new CommandInfo("DELETE", "/session/{sessionId}/cookie/{name}"));
            this.commandDictionary.Add(
                DriverCommand.GetPageSource,
                new CommandInfo("GET", "/session/{sessionId}/source"));
            this.commandDictionary.Add(DriverCommand.GetTitle, new CommandInfo("GET", "/session/{sessionId}/title"));
            this.commandDictionary.Add(
                DriverCommand.FindElement,
                new CommandInfo("POST", "/session/{sessionId}/element"));
            this.commandDictionary.Add(
                DriverCommand.FindElements,
                new CommandInfo("POST", "/session/{sessionId}/elements"));
            this.commandDictionary.Add(
                DriverCommand.GetActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/element/active"));
            this.commandDictionary.Add(
                DriverCommand.FindChildElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/element"));
            this.commandDictionary.Add(
                DriverCommand.FindChildElements,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/elements"));
            this.commandDictionary.Add(
                DriverCommand.DescribeElement,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}"));
            this.commandDictionary.Add(
                DriverCommand.ClickElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/click"));
            this.commandDictionary.Add(
                DriverCommand.GetElementText,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/text"));
            this.commandDictionary.Add(
                DriverCommand.SubmitElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/submit"));
            this.commandDictionary.Add(
                DriverCommand.SendKeysToElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/value"));
            this.commandDictionary.Add(
                DriverCommand.GetElementTagName,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/name"));
            this.commandDictionary.Add(
                DriverCommand.ClearElement,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/clear"));
            this.commandDictionary.Add(
                DriverCommand.IsElementSelected,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/selected"));
            this.commandDictionary.Add(
                DriverCommand.IsElementEnabled,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/enabled"));
            this.commandDictionary.Add(
                DriverCommand.IsElementDisplayed,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/displayed"));
            this.commandDictionary.Add(
                DriverCommand.GetElementLocation,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location"));
            this.commandDictionary.Add(
                DriverCommand.GetElementLocationOnceScrolledIntoView,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/location_in_view"));
            this.commandDictionary.Add(
                DriverCommand.GetElementSize,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/size"));
            this.commandDictionary.Add(
                DriverCommand.GetElementValueOfCssProperty,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/css/{propertyName}"));
            this.commandDictionary.Add(
                DriverCommand.GetElementAttribute,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/attribute/{name}"));
            this.commandDictionary.Add(
                DriverCommand.ElementEquals,
                new CommandInfo("GET", "/session/{sessionId}/element/{id}/equals/{other}"));
            this.commandDictionary.Add(DriverCommand.Close, new CommandInfo("DELETE", "/session/{sessionId}/window"));
            this.commandDictionary.Add(
                DriverCommand.GetWindowSize,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/size"));
            this.commandDictionary.Add(
                DriverCommand.SetWindowSize,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/size"));
            this.commandDictionary.Add(
                DriverCommand.GetWindowPosition,
                new CommandInfo("GET", "/session/{sessionId}/window/{windowHandle}/position"));
            this.commandDictionary.Add(
                DriverCommand.SetWindowPosition,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/position"));
            this.commandDictionary.Add(
                DriverCommand.MaximizeWindow,
                new CommandInfo("POST", "/session/{sessionId}/window/{windowHandle}/maximize"));
            this.commandDictionary.Add(
                DriverCommand.GetOrientation,
                new CommandInfo("GET", "/session/{sessionId}/orientation"));
            this.commandDictionary.Add(
                DriverCommand.SetOrientation,
                new CommandInfo("POST", "/session/{sessionId}/orientation"));
            this.commandDictionary.Add(
                DriverCommand.DismissAlert,
                new CommandInfo("POST", "/session/{sessionId}/dismiss_alert"));
            this.commandDictionary.Add(
                DriverCommand.AcceptAlert,
                new CommandInfo("POST", "/session/{sessionId}/accept_alert"));
            this.commandDictionary.Add(
                DriverCommand.GetAlertText,
                new CommandInfo("GET", "/session/{sessionId}/alert_text"));
            this.commandDictionary.Add(
                DriverCommand.SetAlertValue,
                new CommandInfo("POST", "/session/{sessionId}/alert_text"));
            this.commandDictionary.Add(
                DriverCommand.SetTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts"));
            this.commandDictionary.Add(
                DriverCommand.ImplicitlyWait,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/implicit_wait"));
            this.commandDictionary.Add(
                DriverCommand.SetAsyncScriptTimeout,
                new CommandInfo("POST", "/session/{sessionId}/timeouts/async_script"));
            this.commandDictionary.Add(DriverCommand.MouseClick, new CommandInfo("POST", "/session/{sessionId}/click"));
            this.commandDictionary.Add(
                DriverCommand.MouseDoubleClick,
                new CommandInfo("POST", "/session/{sessionId}/doubleclick"));
            this.commandDictionary.Add(
                DriverCommand.MouseDown,
                new CommandInfo("POST", "/session/{sessionId}/buttondown"));
            this.commandDictionary.Add(DriverCommand.MouseUp, new CommandInfo("POST", "/session/{sessionId}/buttonup"));
            this.commandDictionary.Add(
                DriverCommand.MouseMoveTo,
                new CommandInfo("POST", "/session/{sessionId}/moveto"));
            this.commandDictionary.Add(
                DriverCommand.SendKeysToActiveElement,
                new CommandInfo("POST", "/session/{sessionId}/keys"));
            this.commandDictionary.Add(
                DriverCommand.TouchSingleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/click"));
            this.commandDictionary.Add(
                DriverCommand.TouchPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/down"));
            this.commandDictionary.Add(
                DriverCommand.TouchRelease,
                new CommandInfo("POST", "/session/{sessionId}/touch/up"));
            this.commandDictionary.Add(
                DriverCommand.TouchMove,
                new CommandInfo("POST", "/session/{sessionId}/touch/move"));
            this.commandDictionary.Add(
                DriverCommand.TouchScroll,
                new CommandInfo("POST", "/session/{sessionId}/touch/scroll"));
            this.commandDictionary.Add(
                DriverCommand.TouchDoubleTap,
                new CommandInfo("POST", "/session/{sessionId}/touch/doubleclick"));
            this.commandDictionary.Add(
                DriverCommand.TouchLongPress,
                new CommandInfo("POST", "/session/{sessionId}/touch/longclick"));
            this.commandDictionary.Add(
                DriverCommand.TouchFlick,
                new CommandInfo("POST", "/session/{sessionId}/touch/flick"));
            this.commandDictionary.Add(DriverCommand.UploadFile, new CommandInfo("POST", "/session/{sessionId}/file"));
        }


        private void InitializeFlaNiumCommandDictionary()
        {
            #region ComboBox
            this.commandDictionary.Add(
                DriverCommand.ComboBoxCollapse,
                new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/collapse"));

            this.commandDictionary.Add(
              DriverCommand.ComboBoxExpand,
              new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expand"));

            this.commandDictionary.Add(
              DriverCommand.ComboBoxSelect,
              new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/select/{value}"));

            this.commandDictionary.Add(
              DriverCommand.ComboBoxSelectIndex,
              new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/selectIndex/{value}"));

            this.commandDictionary.Add(
              DriverCommand.ComboBoxSetEditableText,
              new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/setEditableText/{value}"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxIsEditable,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/isEditable"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxIsReadOnly,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/isReadonly"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/value"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxSelectedItems,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/selectedItems"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxSelectedItem,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/selectedItem"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxItems,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/items"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxExpandCollapseState,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/expandCollapseState"));

            this.commandDictionary.Add(
             DriverCommand.ComboBoxEditableText,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/combobox/editableText"));
            #endregion


            #region CheckBox
            this.commandDictionary.Add(
             DriverCommand.CheckBoxToggleState,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/checkbox/toggleState"));
            #endregion


            #region Slider
            this.commandDictionary.Add(
             DriverCommand.SliderMinimum,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/minimum"));

            this.commandDictionary.Add(
             DriverCommand.SliderMaximum,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/maximum"));

            this.commandDictionary.Add(
             DriverCommand.SliderSmallChange,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/smallChange"));

            this.commandDictionary.Add(
             DriverCommand.SliderLargeChange,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/largeChange"));

            this.commandDictionary.Add(
             DriverCommand.SliderGetLargeIncreaseButton,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/getLargeIncreaseButton"));

            this.commandDictionary.Add(
             DriverCommand.SliderGetLargeDecreaseButton,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/getLargeDecreaseButton"));

            this.commandDictionary.Add(
             DriverCommand.SliderGetThumb,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/getThumb"));

            this.commandDictionary.Add(
            DriverCommand.SliderIsOnlyValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/isOnlyValue"));

            this.commandDictionary.Add(
             DriverCommand.SliderGetValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/getValue"));

            this.commandDictionary.Add(
             DriverCommand.SliderSetValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/setValue/{value}"));

            this.commandDictionary.Add(
             DriverCommand.SliderSmallIncrement,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/smallIncrement"));

            this.commandDictionary.Add(
             DriverCommand.SliderSmallDecrement,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/smallDecrement"));

            this.commandDictionary.Add(
             DriverCommand.SliderLargeIncrement,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/largeIncrement"));

            this.commandDictionary.Add(
             DriverCommand.SliderLargeDecrement,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/slider/largeDecrement"));
            #endregion


            #region DataGridView
            this.commandDictionary.Add(
             DriverCommand.DataGridViewHasAddRow,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridView/hasAddRow"));

            this.commandDictionary.Add(
             DriverCommand.DataGridViewGetHeader,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridView/getHeader"));

            this.commandDictionary.Add(
             DriverCommand.DataGridViewGetRows,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridView/getRows"));

            this.commandDictionary.Add(
             DriverCommand.DataGridViewHeaderGetColumns,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridViewHeader/getColumns"));

            this.commandDictionary.Add(
             DriverCommand.DataGridViewRowGetCells,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridViewRow/getCells"));

            this.commandDictionary.Add(
             DriverCommand.DataGridViewCellGetValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridViewCell/getValue"));

            this.commandDictionary.Add(
             DriverCommand.DataGridViewCellSetValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/dataGridViewCell/setValue/{value}"));
            #endregion

            #region Grid
            this.commandDictionary.Add(
             DriverCommand.GridRowCount,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/rowCount"));

            this.commandDictionary.Add(
             DriverCommand.GridColumnCount,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/columnCount"));

            this.commandDictionary.Add(
             DriverCommand.GridColumnHeaders,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/columnHeaders"));

            this.commandDictionary.Add(
             DriverCommand.GridRowHeaders,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/rowHeaders"));

            this.commandDictionary.Add(
             DriverCommand.GridRowOrColumnMajor,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/rowOrColumnMajor"));

            this.commandDictionary.Add(
             DriverCommand.GridGetHeader,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/getHeader"));

            this.commandDictionary.Add(
             DriverCommand.GridGetRows,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/getRows"));

            this.commandDictionary.Add(
             DriverCommand.GridSelectedItems,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/selectedItems"));

            this.commandDictionary.Add(
             DriverCommand.GridSelectedItem,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/selectedItem"));

            this.commandDictionary.Add(
             DriverCommand.GridSelect,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/select/{index}"));

            this.commandDictionary.Add(
             DriverCommand.GridSelectText,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/selectText/{index}/{text}"));

            this.commandDictionary.Add(
             DriverCommand.GridAddToSelection,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/addToSelection/{index}"));

            this.commandDictionary.Add(
             DriverCommand.GridAddToSelectionText,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/addToSelectionText/{index}/{text}"));

            this.commandDictionary.Add(
             DriverCommand.GridRemoveFromSelection,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/removeFromSelection/{index}"));

            this.commandDictionary.Add(
             DriverCommand.GridRemoveFromSelectionText,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/removeFromSelectionText/{index}/{text}"));

            this.commandDictionary.Add(
             DriverCommand.GridGetRowByIndex,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/getRowByIndex/{index}"));

            this.commandDictionary.Add(
             DriverCommand.GridGetRowByValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/getRowByValue/{index}/{text}"));

            this.commandDictionary.Add(
             DriverCommand.GridGetRowsByValue,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/grid/getRowsByValue/{index}/{text}/{count}"));


            this.commandDictionary.Add(
             DriverCommand.GridCellContainingGrid,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridCell/containingGrid"));

            this.commandDictionary.Add(
             DriverCommand.GridCellContainingRow,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridCell/containingRow"));


            this.commandDictionary.Add(
             DriverCommand.GridHeaderColumns,
             new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridHeader/columns"));


            this.commandDictionary.Add(
            DriverCommand.GridRowCells,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridRow/cells"));

            this.commandDictionary.Add(
            DriverCommand.GridRowHeader,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridRow/header"));

            this.commandDictionary.Add(
            DriverCommand.GridRowFindCellByText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridRow/findCellByText/{value}"));

            this.commandDictionary.Add(
            DriverCommand.GridRowScrollIntoView,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/gridRow/scrollIntoView"));
            #endregion

            #region ScrollBar
            this.commandDictionary.Add(
            DriverCommand.ScrollBarBaseValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/scrollBarBase/value"));

            this.commandDictionary.Add(
            DriverCommand.ScrollBarBaseMinimumValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/scrollBarBase/minimumValue"));

            this.commandDictionary.Add(
            DriverCommand.ScrollBarBaseMaximumValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/scrollBarBase/maximumValue"));

            this.commandDictionary.Add(
            DriverCommand.ScrollBarBaseSmallChange,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/scrollBarBase/smallChange"));

            this.commandDictionary.Add(
            DriverCommand.ScrollBarBaseLargeChange,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/scrollBarBase/largeChange"));

            this.commandDictionary.Add(
            DriverCommand.ScrollBarBaseIsReadOnly,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/scrollBarBase/isReadOnly"));


            this.commandDictionary.Add(
            DriverCommand.HorizontalScrollBarScrollLeft,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/horizontalScrollBar/scrollLeft"));

            this.commandDictionary.Add(
            DriverCommand.HorizontalScrollBarScrollRight,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/horizontalScrollBar/scrollRight"));

            this.commandDictionary.Add(
            DriverCommand.HorizontalScrollBarScrollLeftLarge,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/horizontalScrollBar/scrollLeftLarge"));

            this.commandDictionary.Add(
            DriverCommand.HorizontalScrollBarScrollRightLarge,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/horizontalScrollBar/scrollRightLarge"));



            this.commandDictionary.Add(
            DriverCommand.VerticalScrollBarScrollUp,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/verticalScrollBar/scrollUp"));

            this.commandDictionary.Add(
            DriverCommand.VerticalScrollBarScrollDown,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/verticalScrollBar/scrollDown"));

            this.commandDictionary.Add(
            DriverCommand.VerticalScrollBarScrollUpLarge,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/verticalScrollBar/scrollUpLarge"));

            this.commandDictionary.Add(
            DriverCommand.VerticalScrollBarScrollDownLarge,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/verticalScrollBar/scrollDownLarge"));
            #endregion

            #region ProgressBar
            this.commandDictionary.Add(
            DriverCommand.ProgressBarMinimum,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/progressBar/minimum"));

            this.commandDictionary.Add(
            DriverCommand.ProgressBarMaximum,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/progressBar/maximum"));

            this.commandDictionary.Add(
            DriverCommand.ProgressBarValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/progressBar/value"));
            #endregion

            #region ListBox
            this.commandDictionary.Add(
            DriverCommand.ListBoxItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/items"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxSelectedItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/selectedItems"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxSelectedItem,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/selectedItem"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxSelectIndex,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/selectIndex/{index}"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxSelectText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/selectText/{value}"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxAddToSelectionIndex,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/addToSelectionIndex/{index}"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxAddToSelectionText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/addToSelectionText/{value}"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxRemoveFromSelectionIndex,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/removeFromSelectionIndex/{index}"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxRemoveFromSelectionText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBox/removeFromSelectionText/{value}"));
            #endregion


            #region ListBoxItem
            this.commandDictionary.Add(
            DriverCommand.ListBoxItemScrollIntoView,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBoxItem/scrollIntoView"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxItemIsChecked,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBoxItem/isChecked"));

            this.commandDictionary.Add(
            DriverCommand.ListBoxItemSetChecked,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/listBoxItem/setChecked/{value}"));
            #endregion

            #region Menu
            this.commandDictionary.Add(
            DriverCommand.MenuItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/menu/items"));
            #endregion

            #region MenuItem
            this.commandDictionary.Add(
            DriverCommand.MenuItemItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/menuItem/items"));

            this.commandDictionary.Add(
            DriverCommand.MenuItemInvoke,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/menuItem/invoke"));

            this.commandDictionary.Add(
            DriverCommand.MenuItemExpand,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/menuItem/expand"));

            this.commandDictionary.Add(
            DriverCommand.MenuItemCollapse,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/menuItem/collapse"));

            this.commandDictionary.Add(
            DriverCommand.MenuItemIsChecked,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/menuItem/isChecked"));
            #endregion

            #region Button
            this.commandDictionary.Add(
            DriverCommand.ButtonInvoke,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/button/invoke"));
            #endregion

            #region Spinner
            this.commandDictionary.Add(
            DriverCommand.SpinnerMinimum,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/minimum"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerMaximum,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/maximum"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerSmallChange,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/smallChange"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerIsOnlyValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/isOnlyValue"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerGetValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/getValue"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerSetValue,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/setValue/{value}"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerIncrement,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/increment"));

            this.commandDictionary.Add(
            DriverCommand.SpinnerDecrement,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/spinner/decrement"));
            #endregion

            #region Tab
            this.commandDictionary.Add(
            DriverCommand.TabSelectedTabItem,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tab/selectedTabItem"));

            this.commandDictionary.Add(
            DriverCommand.TabSelectedTabItemIndex,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tab/selectedTabItemIndex"));

            this.commandDictionary.Add(
            DriverCommand.TabTabItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tab/tabItems"));

            this.commandDictionary.Add(
            DriverCommand.TabSelectTabItemIndex,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tab/selectTabItemIndex/{index}"));

            this.commandDictionary.Add(
            DriverCommand.TabSelectTabItemText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tab/selectTabItemText/{value}"));
            #endregion

            #region TabItem
            this.commandDictionary.Add(
            DriverCommand.TabItemSelect,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tabItem/select"));

            this.commandDictionary.Add(
            DriverCommand.TabItemAddToSelection,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tabItem/addToSelection"));

            this.commandDictionary.Add(
            DriverCommand.TabItemRemoveFromSelection,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tabItem/removeFromSelection"));
            #endregion

            #region TextBox
            this.commandDictionary.Add(
            DriverCommand.TextBoxGetText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/textBox/getText"));

            this.commandDictionary.Add(
            DriverCommand.TextBoxSetText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/textBox/setText/{value}"));

            this.commandDictionary.Add(
            DriverCommand.TextBoxIsReadOnly,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/textBox/isReadOnly"));

            this.commandDictionary.Add(
            DriverCommand.TextBoxEnter,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/textBox/enter/{value}"));
            #endregion

            #region Thumb
            this.commandDictionary.Add(
            DriverCommand.ThumbSlideHorizontally,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/thumb/slideHorizontally/{index}"));

            this.commandDictionary.Add(
            DriverCommand.ThumbSlideVertically,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/thumb/slideVertically/{index}"));
            #endregion

            #region TitleBar
            this.commandDictionary.Add(
            DriverCommand.TitleBarMinimizeButton,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/titleBar/minimizeButton"));

            this.commandDictionary.Add(
            DriverCommand.TitleBarMaximizeButton,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/titleBar/maximizeButton"));

            this.commandDictionary.Add(
            DriverCommand.TitleBarRestoreButton,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/titleBar/restoreButton"));

            this.commandDictionary.Add(
            DriverCommand.TitleBarCloseButton,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/titleBar/closeButton"));
            #endregion

            #region ToggleButton
            this.commandDictionary.Add(
            DriverCommand.ToggleButtonToggle,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/toggleButton/toggle"));

            this.commandDictionary.Add(
            DriverCommand.ToggleButtonGetToggleState,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/toggleButton/getToggleState"));

            this.commandDictionary.Add(
            DriverCommand.ToggleButtonSetToggleState,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/toggleButton/setToggleState/{value}"));
            #endregion

            #region Tree
            this.commandDictionary.Add(
            DriverCommand.TreeSelectedTreeItem,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tree/selectedTreeItem"));

            this.commandDictionary.Add(
            DriverCommand.TreeItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/tree/items"));
            #endregion

            #region TreeItem
            this.commandDictionary.Add(
            DriverCommand.TreeItemItems,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/items"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemGetText,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/getText"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemExpandCollapseState,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/expandCollapseState"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemExpand,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/expand"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemCollapse,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/collapse"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemSelect,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/select"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemAddToSelection,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/addToSelection"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemRemoveFromSelection,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/removeFromSelection"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemIsChecked,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/isChecked"));

            this.commandDictionary.Add(
            DriverCommand.TreeItemSetChecked,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/treeItem/setChecked/{value}"));
            #endregion

            #region Window
            this.commandDictionary.Add(
            DriverCommand.WindowTitle,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/title"));

            this.commandDictionary.Add(
            DriverCommand.WindowIsModal,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/isModal"));

            this.commandDictionary.Add(
            DriverCommand.WindowTitleBar,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/titleBar"));

            this.commandDictionary.Add(
            DriverCommand.WindowModalWindows,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/modalWindows"));

            this.commandDictionary.Add(
            DriverCommand.WindowPopup,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/popup"));

            this.commandDictionary.Add(
            DriverCommand.WindowContextMenu,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/contextMenu"));

            this.commandDictionary.Add(
            DriverCommand.WindowClose,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/close"));

            this.commandDictionary.Add(
            DriverCommand.WindowMove,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/move/{x}/{y}"));

            this.commandDictionary.Add(
            DriverCommand.WindowSetTransparency,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/setTransparency/{index}"));
            
            this.commandDictionary.Add(
            DriverCommand.WindowGetActiveWindow,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/window/getActiveWindow"));
            #endregion

            #region Calendar
            this.commandDictionary.Add(
            DriverCommand.CalendarSelectedDates,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/calendar/selectedDates"));

            this.commandDictionary.Add(
            DriverCommand.CalendarSelectDate,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/calendar/selectDate/{dateTime}"));

            this.commandDictionary.Add(
            DriverCommand.CalendarAddToSelection,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/calendar/addToSelection/{dateTime}"));
            #endregion

            #region DateTimePicker
            this.commandDictionary.Add(
            DriverCommand.DateTimePickerGetDate,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/dateTimePicker/getDate"));

            this.commandDictionary.Add(
            DriverCommand.DateTimePickerSetDate,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/dateTimePicker/setDate/{dateTime}"));
            #endregion

            #region Other

            this.commandDictionary.Add(
            DriverCommand.CustomScreenshot,
            new CommandInfo("POST", "/session/{sessionId}/customScreenshot/{format}"));

            this.commandDictionary.Add(
            DriverCommand.ElementScreenshot,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/elementScreenshot"));

            this.commandDictionary.Add(
            DriverCommand.DragAndDrop,
            new CommandInfo("POST", "/session/{sessionId}/dragAndDrop"));

            this.commandDictionary.Add(
            DriverCommand.GetActiveWindow,
            new CommandInfo("POST", "/session/{sessionId}/getActiveWindow"));

            this.commandDictionary.Add(
            DriverCommand.ElementDragAndDrop,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/elementDragAndDrop"));

            this.commandDictionary.Add(
            DriverCommand.SendCharsToActiveElement,
            new CommandInfo("POST", "/session/{sessionId}/sendCharsToActiveElement"));

            this.commandDictionary.Add(
            DriverCommand.GetKeyboardLayout,
            new CommandInfo("POST", "/session/{sessionId}/getKeyboardLayout"));

            this.commandDictionary.Add(
            DriverCommand.SetKeyboardLayout,
            new CommandInfo("POST", "/session/{sessionId}/setKeyboardLayout"));

            this.commandDictionary.Add(
            DriverCommand.ElementMouseAction,
            new CommandInfo("POST", "/session/{sessionId}/element/{id}/elementMouseAction"));

            this.commandDictionary.Add(
            DriverCommand.GetClipboardText,
            new CommandInfo("POST", "/session/{sessionId}/getClipboardText"));

            this.commandDictionary.Add(
            DriverCommand.SetClipboardText,
            new CommandInfo("POST", "/session/{sessionId}/setClipboardText"));

            this.commandDictionary.Add(
            DriverCommand.KeyCombination,
            new CommandInfo("POST", "/session/{sessionId}/keyCombination"));

            #endregion

        }

        #endregion
    }
}
