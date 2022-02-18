Option Explicit On
Option Strict On

Public Class cFITSStatistics

  Public Shared Sub GetStatistics(ByVal FileList As List(Of String), ByVal ReportDirectory As String)

    Dim Loader As New cFITSReader

    Dim Data(,) As Double = Nothing

    Dim DataMax(,) As Double = Nothing
    Dim DataMin(,) As Double = Nothing
    Dim DataSum(,) As Double = Nothing
    Dim DataSquareSum(,) As Double = Nothing

    Dim IsInit As Boolean = False

    For Each FitsFile As String In FileList
      Data = Nothing
      GC.Collect()
      Loader.ReadIn(FitsFile, Data)
      If IsInit = False Then
        ReDim DataMax(Data.GetUpperBound(0), Data.GetUpperBound(1))
        ReDim DataMin(Data.GetUpperBound(0), Data.GetUpperBound(1))
        ReDim DataSum(Data.GetUpperBound(0), Data.GetUpperBound(1))
        ReDim DataSquareSum(Data.GetUpperBound(0), Data.GetUpperBound(1))
      End If
      Dim TotalMean As Double = 0
      For Idx1 As Integer = 0 To Data.GetUpperBound(0)
        For Idx2 As Integer = 0 To Data.GetUpperBound(1)
          TotalMean += Data(Idx1, Idx2)
          If IsInit = False Then
            DataMax(Idx1, Idx2) = Data(Idx1, Idx2)
            DataMin(Idx1, Idx2) = Data(Idx1, Idx2)
            DataSum(Idx1, Idx2) = Data(Idx1, Idx2)
            DataSquareSum(Idx1, Idx2) = Data(Idx1, Idx2) * Data(Idx1, Idx2)
          Else
            If Data(Idx1, Idx2) > DataMax(Idx1, Idx2) Then DataMax(Idx1, Idx2) = Data(Idx1, Idx2)
            If Data(Idx1, Idx2) < DataMin(Idx1, Idx2) Then DataMin(Idx1, Idx2) = Data(Idx1, Idx2)
            DataSum(Idx1, Idx2) += Data(Idx1, Idx2)
            DataSquareSum(Idx1, Idx2) += Data(Idx1, Idx2) * Data(Idx1, Idx2)
          End If
        Next Idx2
      Next Idx1
      TotalMean /= Data.Length
      IsInit = True
    Next FitsFile

    'Normalize sum and square sum
    For Idx1 As Integer = 0 To Data.GetUpperBound(0)
      For Idx2 As Integer = 0 To Data.GetUpperBound(1)
        DataSum(Idx1, Idx2) = DataSum(Idx1, Idx2) / Data.Length
        DataSquareSum(Idx1, Idx2) = DataSquareSum(Idx1, Idx2) / Data.Length
      Next Idx2
    Next Idx1

    'Report range statistics
    Dim MaxCSV As New List(Of String)
    Dim MinCSV As New List(Of String)
    Dim MeanCSV As New List(Of String)
    Dim SigmaCSV As New List(Of String)

    Dim MeanVsSigma As New List(Of String)

    For Idx1 As Integer = 0 To DataMax.GetUpperBound(0)
      Dim MaxCSVLine As New List(Of String)
      Dim MinCSVLine As New List(Of String)
      Dim MeanCSVLine As New List(Of String)
      Dim SigmaCSVLine As New List(Of String)
      For Idx2 As Integer = 0 To DataMax.GetUpperBound(1)
        Dim Sigma As Double = (DataSquareSum(Idx1, Idx2) - (DataSum(Idx1, Idx2) * DataSum(Idx1, Idx2)))
        MaxCSVLine.Add(CSVFormat(DataMax(Idx1, Idx2)))
        MinCSVLine.Add(CSVFormat(DataMin(Idx1, Idx2)))
        MeanCSVLine.Add(CSVFormat(DataSum(Idx1, Idx2)))
        SigmaCSVLine.Add(CSVFormat(Sigma))
        MeanVsSigma.Add(CSVFormat(DataSum(Idx1, Idx2)) & ";" & CSVFormat(Sigma))
      Next Idx2
      Dim EndString As String = CStr(IIf(Idx1 <> Data.GetUpperBound(0), System.Environment.NewLine, String.Empty))
      System.IO.File.AppendAllText(ReportDirectory & "\Statistics_MAX.csv", Join(MaxCSVLine.ToArray, ";" & EndString))
      System.IO.File.AppendAllText(ReportDirectory & "\Statistics_MIN.csv", Join(MinCSVLine.ToArray, ";" & EndString))
      System.IO.File.AppendAllText(ReportDirectory & "\Statistics_MEAN.csv", Join(MeanCSVLine.ToArray, ";" & EndString))
      System.IO.File.AppendAllText(ReportDirectory & "\Statistics_SIGMA.csv", Join(SigmaCSVLine.ToArray, ";" & EndString))
    Next Idx1

    System.IO.File.WriteAllLines(ReportDirectory & "\Statistics_MeanVsSigma.csv", MeanVsSigma.ToArray)

    Process.Start(ReportDirectory)

  End Sub

  Private Shared Function CSVFormat(ByVal Value As Double) As String
    Return Str(Value).Trim.Replace(".", Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
  End Function

End Class
