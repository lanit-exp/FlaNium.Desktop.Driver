namespace FlaNium.Desktop.Driver.Common {

    /// <summary>
    /// Values describing the list of commands understood by a remote server using the JSON wire protocol.
    /// 
    /// </summary>
    public static class DriverCommand {

        #region Static Fields

        #region Selenium

        /// <summary>
        /// Represents the AcceptAlert command
        /// 
        /// </summary>
        public static readonly string AcceptAlert = "acceptAlert";

        /// <summary>
        /// Represents adding a cookie command
        /// 
        /// </summary>
        public static readonly string AddCookie = "addCookie";

        /// <summary>
        /// Represents ClearElement command
        /// 
        /// </summary>
        public static readonly string ClearElement = "clearElement";

        /// <summary>
        /// Represents ClickElement command
        /// 
        /// </summary>
        public static readonly string ClickElement = "clickElement";

        /// <summary>
        /// Represents a Browser close command
        /// 
        /// </summary>
        public static readonly string Close = "close";

        /// <summary>
        /// Represents the Define Driver Mapping command
        /// 
        /// </summary>
        public static readonly string DefineDriverMapping = "defineDriverMapping";

        /// <summary>
        /// Represents Deleting all cookies command
        /// 
        /// </summary>
        public static readonly string DeleteAllCookies = "deleteAllCookies";

        /// <summary>
        /// Represents deleting a cookie command
        /// 
        /// </summary>
        public static readonly string DeleteCookie = "deleteCookie";

        /// <summary>
        /// Describes an element
        /// 
        /// </summary>
        public static readonly string DescribeElement = "describeElement";

        /// <summary>
        /// Represents the DismissAlert command
        /// 
        /// </summary>
        public static readonly string DismissAlert = "dismissAlert";

        /// <summary>
        /// Represents ElementEquals command
        /// 
        /// </summary>
        public static readonly string ElementEquals = "elementEquals";

        /// <summary>
        /// Represents ExecuteAsyncScript command
        /// 
        /// </summary>
        public static readonly string ExecuteAsyncScript = "executeAsyncScript";

        /// <summary>
        /// Represents ExecuteScript command
        /// 
        /// </summary>
        public static readonly string ExecuteScript = "executeScript";

        /// <summary>
        /// Represents FindChildElement command
        /// 
        /// </summary>
        public static readonly string FindChildElement = "findChildElement";

        /// <summary>
        /// Represents FindChildElements command
        /// 
        /// </summary>
        public static readonly string FindChildElements = "findChildElements";

        /// <summary>
        /// Represents FindElement command
        /// 
        /// </summary>
        public static readonly string FindElement = "findElement";

        /// <summary>
        /// Represents FindElements command
        /// 
        /// </summary>
        public static readonly string FindElements = "findElements";

        /// <summary>
        /// Represents a GET command
        /// 
        /// </summary>
        public static readonly string Get = "get";

        /// <summary>
        /// Represents GetActiveElement command
        /// 
        /// </summary>
        public static readonly string GetActiveElement = "getActiveElement";

        /// <summary>
        /// Represents the GetAlertText command
        /// 
        /// </summary>
        public static readonly string GetAlertText = "getAlertText";

        /// <summary>
        /// Represents getting all cookies command
        /// 
        /// </summary>
        public static readonly string GetAllCookies = "getCookies";

        /// <summary>
        /// Represents GetCurrentUrl command
        /// 
        /// </summary>
        public static readonly string GetCurrentUrl = "getCurrentUrl";

        /// <summary>
        /// Represents GetCurrentWindowHandle command
        /// 
        /// </summary>
        public static readonly string GetCurrentWindowHandle = "getCurrentWindowHandle";

        /// <summary>
        /// Represents GetElementAttribute command
        /// 
        /// </summary>
        public static readonly string GetElementAttribute = "getElementAttribute";

        /// <summary>
        /// Represents GetElementLocation command
        /// 
        /// </summary>
        public static readonly string GetElementLocation = "getElementLocation";

        /// <summary>
        /// Represents GetElementLocationOnceScrolledIntoView command
        /// 
        /// </summary>
        public static readonly string GetElementLocationOnceScrolledIntoView = "getElementLocationOnceScrolledIntoView";

        /// <summary>
        /// Represents GetElementSize command
        /// 
        /// </summary>
        public static readonly string GetElementSize = "getElementSize";

        /// <summary>
        /// Represents GetElementTagName command
        /// 
        /// </summary>
        public static readonly string GetElementTagName = "getElementTagName";

        /// <summary>
        /// Represents GetElementText command
        /// 
        /// </summary>
        public static readonly string GetElementText = "getElementText";

        /// <summary>
        /// Represents GetElementValueOfCSSProperty command
        /// 
        /// </summary>
        public static readonly string GetElementValueOfCssProperty = "getElementValueOfCssProperty";

        /// <summary>
        /// Represents GetOrientation command
        /// 
        /// </summary>
        public static readonly string GetOrientation = "getOrientation";

        /// <summary>
        /// Represents GetPageSource command
        /// 
        /// </summary>
        public static readonly string GetPageSource = "getPageSource";

        /// <summary>
        /// Represents the Get Session Capabilities command
        /// 
        /// </summary>
        public static readonly string GetSessionCapabilities = "getSessionCapabilities";

        /// <summary>
        /// Represents the Get Session List command
        /// 
        /// </summary>
        public static readonly string GetSessionList = "getSessionList";

        /// <summary>
        /// Represents GetTitle command
        /// 
        /// </summary>
        public static readonly string GetTitle = "getTitle";

        /// <summary>
        /// Represents GetWindowHandles command
        /// 
        /// </summary>
        public static readonly string GetWindowHandles = "getWindowHandles";

        /// <summary>
        /// Represents GetWindowPosition command
        /// 
        /// </summary>
        public static readonly string GetWindowPosition = "getWindowPosition";

        /// <summary>
        /// Represents GetWindowSize command
        /// 
        /// </summary>
        public static readonly string GetWindowSize = "getWindowSize";

        /// <summary>
        /// Represents a Browser going back command
        /// 
        /// </summary>
        public static readonly string GoBack = "goBack";

        /// <summary>
        /// Represents a Browser going forward command
        /// 
        /// </summary>
        public static readonly string GoForward = "goForward";

        /// <summary>
        /// Represents the ImplicitlyWait command
        /// 
        /// </summary>
        public static readonly string ImplicitlyWait = "implicitlyWait";

        /// <summary>
        /// Represents IsElementDisplayed command
        /// 
        /// </summary>
        public static readonly string IsElementDisplayed = "isElementDisplayed";

        /// <summary>
        /// Represents IsElementEnabled command
        /// 
        /// </summary>
        public static readonly string IsElementEnabled = "isElementEnabled";

        /// <summary>
        /// Represents IsElementSelected command
        /// 
        /// </summary>
        public static readonly string IsElementSelected = "isElementSelected";

        /// <summary>
        /// Represents MaximizeWindow command
        /// 
        /// </summary>
        public static readonly string MaximizeWindow = "maximizeWindow";

        /// <summary>
        /// Represents the MouseClick command.
        /// 
        /// </summary>
        public static readonly string MouseClick = "mouseClick";

        /// <summary>
        /// Represents the MouseDoubleClick command.
        /// 
        /// </summary>
        public static readonly string MouseDoubleClick = "mouseDoubleClick";

        /// <summary>
        /// Represents the MouseDown command.
        /// 
        /// </summary>
        public static readonly string MouseDown = "mouseDown";

        /// <summary>
        /// Represents the MouseMoveTo command.
        /// 
        /// </summary>
        public static readonly string MouseMoveTo = "mouseMoveTo";

        /// <summary>
        /// Represents the MouseUp command.
        /// 
        /// </summary>
        public static readonly string MouseUp = "mouseUp";

        /// <summary>
        /// Represents a New Session command
        /// 
        /// </summary>
        public static readonly string NewSession = "newSession";

        /// <summary>
        /// Represents a browser quit command
        /// 
        /// </summary>
        public static readonly string Quit = "quit";

        /// <summary>
        /// Represents a Browser refreshing command
        /// 
        /// </summary>
        public static readonly string Refresh = "refresh";

        /// <summary>
        /// Represents Screenshot command
        /// 
        /// </summary>
        public static readonly string Screenshot = "screenshot";

        /// <summary>
        /// Represents the SendKeysToActiveElement command.
        /// 
        /// </summary>
        public static readonly string SendKeysToActiveElement = "sendKeysToActiveElement";

        /// <summary>
        /// Represents SendKeysToElements command
        /// 
        /// </summary>
        public static readonly string SendKeysToElement = "sendKeysToElement";

        /// <summary>
        /// Represents the SetAlertValue command
        /// 
        /// </summary>
        public static readonly string SetAlertValue = "setAlertValue";

        /// <summary>
        /// Represents the SetAsyncScriptTimeout command
        /// 
        /// </summary>
        public static readonly string SetAsyncScriptTimeout = "setScriptTimeout";

        /// <summary>
        /// Represents SetOrientation command
        /// 
        /// </summary>
        public static readonly string SetOrientation = "setOrientation";

        /// <summary>
        /// Represents the SetTimeout command
        /// 
        /// </summary>
        public static readonly string SetTimeout = "setTimeout";

        /// <summary>
        /// Represents SetWindowPosition command
        /// 
        /// </summary>
        public static readonly string SetWindowPosition = "setWindowPosition";

        /// <summary>
        /// Represents SetWindowSize command
        /// 
        /// </summary>
        public static readonly string SetWindowSize = "setWindowSize";

        /// <summary>
        /// Represents the Status command.
        /// 
        /// </summary>
        public static readonly string Status = "status";

        /// <summary>
        /// Represents SubmitElement command
        /// 
        /// </summary>
        public static readonly string SubmitElement = "submitElement";

        /// <summary>
        /// Represents SwitchToFrame command
        /// 
        /// </summary>
        public static readonly string SwitchToFrame = "switchToFrame";

        /// <summary>
        /// Represents SwitchToParentFrame command
        /// 
        /// </summary>
        public static readonly string SwitchToParentFrame = "switchToParentFrame";

        /// <summary>
        /// Represents SwitchToWindow command
        /// 
        /// </summary>
        public static readonly string SwitchToWindow = "switchToWindow";

        /// <summary>
        /// Represents the TouchDoubleTap command.
        /// 
        /// </summary>
        public static readonly string TouchDoubleTap = "touchDoubleTap";

        /// <summary>
        /// Represents the TouchFlick command.
        /// 
        /// </summary>
        public static readonly string TouchFlick = "touchFlick";

        /// <summary>
        /// Represents the TouchLongPress command.
        /// 
        /// </summary>
        public static readonly string TouchLongPress = "touchLongPress";

        /// <summary>
        /// Represents the TouchMove command.
        /// 
        /// </summary>
        public static readonly string TouchMove = "touchMove";

        /// <summary>
        /// Represents the TouchPress command.
        /// 
        /// </summary>
        public static readonly string TouchPress = "touchDown";

        /// <summary>
        /// Represents the TouchRelease command.
        /// 
        /// </summary>
        public static readonly string TouchRelease = "touchUp";

        /// <summary>
        /// Represents the TouchScroll command.
        /// 
        /// </summary>
        public static readonly string TouchScroll = "touchScroll";

        /// <summary>
        /// Represents the TouchSingleTap command.
        /// 
        /// </summary>
        public static readonly string TouchSingleTap = "touchSingleTap";

        /// <summary>
        /// Represents the UploadFile command.
        /// 
        /// </summary>
        public static readonly string UploadFile = "uploadFile";

        public static readonly string Actions = "actions";

        #endregion


        #region FlaNium

        public static readonly string ExecuteInApp = "executeInApp";

        #region ComboBox

        public static readonly string ComboBoxCollapse = "comboBoxCollapse";
        public static readonly string ComboBoxExpand = "comboBoxExpand";
        public static readonly string ComboBoxSelect = "comboBoxSelect";
        public static readonly string ComboBoxSelectIndex = "comboBoxSelectIndex";
        public static readonly string ComboBoxSetEditableText = "comboBoxSetEditableText";
        public static readonly string ComboBoxIsEditable = "comboBoxIsEditable";
        public static readonly string ComboBoxIsReadOnly = "comboBoxIsReadOnly";
        public static readonly string ComboBoxValue = "comboBoxValue";
        public static readonly string ComboBoxSelectedItems = "comboBoxSelectedItems";
        public static readonly string ComboBoxSelectedItem = "comboBoxSelectedItem";
        public static readonly string ComboBoxItems = "comboBoxItems";
        public static readonly string ComboBoxExpandCollapseState = "comboBoxExpandCollapseState";
        public static readonly string ComboBoxEditableText = "comboBoxEditableText";

        #endregion

        #region CheckBox

        public static readonly string CheckBoxToggleState = "checkBoxToggleState";

        #endregion

        #region Slider

        public static readonly string SliderMinimum = "sliderMinimum";
        public static readonly string SliderMaximum = "sliderMaximum";
        public static readonly string SliderSmallChange = "sliderSmallChange";
        public static readonly string SliderLargeChange = "sliderLargeChange";
        public static readonly string SliderGetLargeIncreaseButton = "sliderGetLargeIncreaseButton";
        public static readonly string SliderGetLargeDecreaseButton = "sliderGetLargeDecreaseButton";
        public static readonly string SliderGetThumb = "sliderGetThumb";
        public static readonly string SliderIsOnlyValue = "sliderIsOnlyValue";
        public static readonly string SliderGetValue = "sliderGetValue";
        public static readonly string SliderSetValue = "sliderSetValue";
        public static readonly string SliderSmallIncrement = "sliderSmallIncrement";
        public static readonly string SliderSmallDecrement = "sliderSmallDecrement";
        public static readonly string SliderLargeIncrement = "sliderLargeIncrement";
        public static readonly string SliderLargeDecrement = "sliderLargeDecrement";

        #endregion

        #region DataGridView

        public static readonly string DataGridViewHasAddRow = "dataGridViewHasAddRow";
        public static readonly string DataGridViewGetHeader = "dataGridViewGetHeader";
        public static readonly string DataGridViewGetRows = "dataGridViewGetRows";

        public static readonly string DataGridViewHeaderGetColumns = "dataGridViewHeaderGetColumns";

        public static readonly string DataGridViewRowGetCells = "dataGridViewRowGetCells";

        public static readonly string DataGridViewCellGetValue = "dataGridViewCellGetValue";
        public static readonly string DataGridViewCellSetValue = "dataGridViewCellSetValue";

        #endregion

        #region Grid

        public static readonly string GridRowCount = "gridRowCount";
        public static readonly string GridColumnCount = "gridColumnCount";
        public static readonly string GridColumnHeaders = "gridColumnHeaders";
        public static readonly string GridRowHeaders = "gridRowHeaders";
        public static readonly string GridRowOrColumnMajor = "gridRowOrColumnMajor";
        public static readonly string GridGetHeader = "gridGetHeader";
        public static readonly string GridGetRows = "gridGetRows";
        public static readonly string GridSelectedItems = "gridSelectedItems";
        public static readonly string GridSelectedItem = "gridSelectedItem";
        public static readonly string GridSelect = "gridSelect";
        public static readonly string GridSelectText = "gridSelectText";
        public static readonly string GridAddToSelection = "gridAddToSelection";
        public static readonly string GridAddToSelectionText = "gridAddToSelectionText";
        public static readonly string GridRemoveFromSelection = "gridRemoveFromSelection";
        public static readonly string GridRemoveFromSelectionText = "gridRemoveFromSelectionText";
        public static readonly string GridGetRowByIndex = "gridGetRowByIndex";
        public static readonly string GridGetRowByValue = "gridGetRowByValue";
        public static readonly string GridGetRowsByValue = "gridGetRowsByValue";

        public static readonly string GridCellContainingGrid = "gridCellContainingGrid";
        public static readonly string GridCellContainingRow = "gridCellContainingRow";

        public static readonly string GridHeaderColumns = "gridHeaderColumns";

        public static readonly string GridRowCells = "gridRowCells";
        public static readonly string GridRowHeader = "gridRowHeader";
        public static readonly string GridRowFindCellByText = "gridRowFindCellByText";
        public static readonly string GridRowScrollIntoView = "gridRowScrollIntoView";

        #endregion

        #region ScrollBar

        public static readonly string ScrollBarBaseValue = "scrollBarBaseValue";
        public static readonly string ScrollBarBaseMinimumValue = "scrollBarBaseMinimumValue";
        public static readonly string ScrollBarBaseMaximumValue = "scrollBarBaseMaximumValue";
        public static readonly string ScrollBarBaseSmallChange = "scrollBarBaseSmallChange";
        public static readonly string ScrollBarBaseLargeChange = "scrollBarBaseLargeChange";
        public static readonly string ScrollBarBaseIsReadOnly = "scrollBarBaseIsReadOnly";

        public static readonly string HorizontalScrollBarScrollLeft = "horizontalScrollBarScrollLeft";
        public static readonly string HorizontalScrollBarScrollRight = "horizontalScrollBarScrollRight";
        public static readonly string HorizontalScrollBarScrollLeftLarge = "horizontalScrollBarScrollLeftLarge";
        public static readonly string HorizontalScrollBarScrollRightLarge = "horizontalScrollBarScrollRightLarge";

        public static readonly string VerticalScrollBarScrollUp = "verticalScrollBarScrollUp";
        public static readonly string VerticalScrollBarScrollDown = "verticalScrollBarScrollDown";
        public static readonly string VerticalScrollBarScrollUpLarge = "verticalScrollBarScrollUpLarge";
        public static readonly string VerticalScrollBarScrollDownLarge = "verticalScrollBarScrollDownLarge";

        #endregion

        #region ProgressBar

        public static readonly string ProgressBarMinimum = "progressBarMinimum";
        public static readonly string ProgressBarMaximum = "progressBarMaximum";
        public static readonly string ProgressBarValue = "progressBarValue";

        #endregion

        #region ListBox

        public static readonly string ListBoxItems = "listBoxItems";
        public static readonly string ListBoxSelectedItems = "listBoxSelectedItems";
        public static readonly string ListBoxSelectedItem = "listBoxSelectedItem";
        public static readonly string ListBoxSelectIndex = "listBoxSelectIndex";
        public static readonly string ListBoxSelectText = "listBoxSelectText";
        public static readonly string ListBoxAddToSelectionIndex = "listBoxAddToSelectionIndex";
        public static readonly string ListBoxAddToSelectionText = "listBoxAddToSelectionText";
        public static readonly string ListBoxRemoveFromSelectionIndex = "listBoxRemoveFromSelectionIndex";
        public static readonly string ListBoxRemoveFromSelectionText = "listBoxRemoveFromSelectionText";

        public static readonly string ListBoxItemScrollIntoView = "listBoxItemScrollIntoView";
        public static readonly string ListBoxItemIsChecked = "listBoxItemIsChecked";
        public static readonly string ListBoxItemSetChecked = "listBoxItemSetChecked";

        #endregion

        #region Menu

        public static readonly string MenuItems = "menuItems";

        #endregion

        #region MenuItem

        public static readonly string MenuItemItems = "menuItemItems";
        public static readonly string MenuItemInvoke = "menuItemInvoke";
        public static readonly string MenuItemExpand = "menuItemExpand";
        public static readonly string MenuItemCollapse = "menuItemCollapse";
        public static readonly string MenuItemIsChecked = "menuItemIsChecked";

        #endregion

        #region Button

        public static readonly string ButtonInvoke = "buttonInvoke";

        #endregion

        #region Spinner

        public static readonly string SpinnerMinimum = "spinnerMinimum";
        public static readonly string SpinnerMaximum = "spinnerMaximum";
        public static readonly string SpinnerSmallChange = "spinnerSmallChange";
        public static readonly string SpinnerIsOnlyValue = "spinnerIsOnlyValue";
        public static readonly string SpinnerGetValue = "spinnerGetValue";
        public static readonly string SpinnerSetValue = "spinnerSetValue";
        public static readonly string SpinnerIncrement = "spinnerIncrement";
        public static readonly string SpinnerDecrement = "spinnerDecrement";

        #endregion

        #region Tab

        public static readonly string TabSelectedTabItem = "tabSelectedTabItem";
        public static readonly string TabSelectedTabItemIndex = "tabSelectedTabItemIndex";
        public static readonly string TabTabItems = "tabTabItems";
        public static readonly string TabSelectTabItemIndex = "tabSelectTabItemIndex";
        public static readonly string TabSelectTabItemText = "tabSelectTabItemText";

        #endregion

        #region TabItem

        public static readonly string TabItemSelect = "tabItemSelect";
        public static readonly string TabItemAddToSelection = "tabItemAddToSelection";
        public static readonly string TabItemRemoveFromSelection = "tabItemRemoveFromSelection";

        #endregion

        #region TextBox

        public static readonly string TextBoxGetText = "textBoxGetText";
        public static readonly string TextBoxSetText = "textBoxSetText";
        public static readonly string TextBoxIsReadOnly = "textBoxIsReadOnly";
        public static readonly string TextBoxEnter = "textBoxEnter";

        #endregion

        #region Thumb

        public static readonly string ThumbSlideHorizontally = "thumbSlideHorizontally";
        public static readonly string ThumbSlideVertically = "thumbSlideVertically";

        #endregion

        #region TitleBar

        public static readonly string TitleBarMinimizeButton = "titleBarMinimizeButton";
        public static readonly string TitleBarMaximizeButton = "titleBarMaximizeButton";
        public static readonly string TitleBarRestoreButton = "titleBarRestoreButton";
        public static readonly string TitleBarCloseButton = "titleBarCloseButton";

        #endregion

        #region ToggleButton

        public static readonly string ToggleButtonToggle = "toggleButtonToggle";
        public static readonly string ToggleButtonGetToggleState = "toggleButtonGetToggleState";
        public static readonly string ToggleButtonSetToggleState = "toggleButtonSetToggleState";

        #endregion

        #region Tree

        public static readonly string TreeSelectedTreeItem = "treeSelectedTreeItem";
        public static readonly string TreeItems = "treeItems";

        #endregion

        #region TreeItem

        public static readonly string TreeItemItems = "treeItemItems";
        public static readonly string TreeItemGetText = "treeItemGetText";
        public static readonly string TreeItemExpandCollapseState = "treeItemExpandCollapseState";
        public static readonly string TreeItemExpand = "treeItemExpand";
        public static readonly string TreeItemCollapse = "treeItemCollapse";
        public static readonly string TreeItemSelect = "treeItemSelect";
        public static readonly string TreeItemAddToSelection = "treeItemAddToSelection";
        public static readonly string TreeItemRemoveFromSelection = "treeItemRemoveFromSelection";
        public static readonly string TreeItemIsChecked = "treeItemIsChecked";
        public static readonly string TreeItemSetChecked = "treeItemSetChecked";

        #endregion

        #region Window

        public static readonly string WindowTitle = "windowTitle";
        public static readonly string WindowIsModal = "windowIsModal";
        public static readonly string WindowTitleBar = "windowTitleBar";
        public static readonly string WindowModalWindows = "windowModalWindows";
        public static readonly string WindowPopup = "windowPopup";
        public static readonly string WindowContextMenu = "windowContextMenu";
        public static readonly string WindowClose = "windowClose";
        public static readonly string WindowMove = "windowMove";
        public static readonly string WindowSetTransparency = "windowSetTransparency";
        public static readonly string WindowGetActiveWindow = "windowGetActiveWindow";

        #endregion

        #region Calendar

        public static readonly string CalendarSelectedDates = "calendarSelectedDates";
        public static readonly string CalendarSelectDate = "calendarSelectDate";
        public static readonly string CalendarAddToSelection = "calendarAddToSelection";

        #endregion

        #region DateTimePicker

        public static readonly string DateTimePickerGetDate = "dateTimePickerGetDate";
        public static readonly string DateTimePickerSetDate = "dateTimePickerSetDate";

        #endregion

        #region Other

        public static readonly string CustomScreenshot = "customScreenshot";
        public static readonly string ElementScreenshot = "elementScreenshot";
        public static readonly string DragAndDrop = "DragAndDrop";
        public static readonly string GetActiveWindow = "GetActiveWindow";
        public static readonly string ElementDragAndDrop = "ElementDragAndDrop";
        public static readonly string SendCharsToActiveElement = "SendCharsToActiveElement";
        public static readonly string GetKeyboardLayout = "GetKeyboardLayout";
        public static readonly string SetKeyboardLayout = "SetKeyboardLayout";
        public static readonly string ElementMouseAction = "ElementMouseAction";
        public static readonly string GetClipboardText = "GetClipboardText";
        public static readonly string SetClipboardText = "SetClipboardText";
        public static readonly string KeyCombination = "KeyCombination";

        public static readonly string TouchActionsTap = "TouchActionsTap";
        public static readonly string TouchActionsHold = "TouchActionsHold";
        public static readonly string TouchActionsPinch = "TouchActionsPinch";
        public static readonly string TouchActionsTransition = "TouchActionsTransition";
        public static readonly string TouchActionsDrag = "TouchActionsDrag";
        public static readonly string TouchActionsRotate = "TouchActionsRotate";

        public static readonly string SetRootElement = "SetRootElement";
        
        public static readonly string ChangeProcess = "ChangeProcess";
        public static readonly string KillProcesses = "KillProcesses";
        
        public static readonly string FileOrDirectoryExists = "FileOrDirectoryExists";
        public static readonly string DeleteFileOrDirectory = "DeleteFileOrDirectory";
        #endregion

        #endregion

        #endregion

    }

}