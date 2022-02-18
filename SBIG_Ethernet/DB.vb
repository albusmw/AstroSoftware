Option Explicit On
Option Strict On

Imports System.ComponentModel

Public Class cDB

    <Category("1.) General")>
    <DisplayName("a) IP adress")>
    <DefaultValue("192.168.10.220")>
    Public Property IP() As String = "192.168.10.220"

    <Category("1.) General")>
    <DisplayName("b) Update interval")>
    <DefaultValue(1000)>
    Public Property UpdateInterval() As Integer = 1000

    <Category("2.) Exposure")>
    <DisplayName("a) Expose time [s]")>
    <DefaultValue(1.0)>
    Public Property ExposeTime() As Double = 1.0

    <Category("2.) Exposure")>
    <DisplayName("b) Expose count [1]")>
    <DefaultValue(1)>
    Public Property ExposeCount() As Integer = 1

    <Category("2.) Exposure")>
    <DisplayName("b) Binning")>
    <DefaultValue("1x1")>
    Public Property Binning() As String = "1x1"

    <Category("3.) File operation")>
    <DisplayName("a) Storage root")>
    <DefaultValue("C:\DATA_IO")>
    Public Property StorageRoot() As String = "C:\DATA_IO"

    <Category("3.) File operation")>
    <DisplayName("b) Base file name")>
    <DefaultValue("CurrentImage")>
    Public Property FileBaseName() As String = "CurrentImage"

    <Category("3.) File operation")>
    <DisplayName("c) Timestamp format")>
    <DefaultValue("yyyy-MM-dd_HH-mm-ss-fff")>
    Public Property StorageTimeStamp() As String = "yyyy-MM-dd_HH-mm-ss-fff"

    <Category("3.) File operation")>
    <DisplayName("d) Auto-open last file")>
    <DefaultValue(True)>
    Public Property AutoOpenLast() As Boolean = True

    <Category("4.) ROI")>
    <DisplayName("a) StartX")>
    <DefaultValue(0)>
    Public Property StartX() As Integer = 0

    <Category("4.) ROI")>
    <DisplayName("b) StartY")>
    <DefaultValue(0)>
    Public Property StartY() As Integer = 0

    <Category("4.) ROI")>
    <DisplayName("c) NumX")>
    <DefaultValue(True)>
    Public Property NumX() As Integer = 4096

    <Category("4.) ROI")>
    <DisplayName("d) NumY")>
    <DefaultValue(True)>
    Public Property NumY() As Integer = 4096

End Class
