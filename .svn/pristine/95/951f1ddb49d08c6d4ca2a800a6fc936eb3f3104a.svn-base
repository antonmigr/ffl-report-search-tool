' Create WScript Shell Object to access filesystem.
'Set WshShell = WScript.CreateObject("WScript.Shell")
						
'WshShell.SendKeys "%(Fax)"

' Wait for 1 second1
'WScript.Sleep 1000

'WshShell.SendKeys "{ENTER}"



On Error Resume Next



Set args = WScript.Arguments

'// you can get url via parameter like line below
Url = args.Item(0)

 'Url = "http://extranet/sites/08102034/Shared%20Documents/S0385978_584367_8569924_1649019_ID_labreport.pdf"
dim xHttp: Set xHttp = createobject("Microsoft.XMLHTTP")
dim bStrm: Set bStrm = createobject("Adodb.Stream")
xHttp.Open "GET", Url, False
xHttp.Send
 
with bStrm
    .type = 1 '//binary
    .open
    .write xHttp.responseBody
       .savetofile args.Item(2)&args.Item(1)&".pdf", 2 '//overwrite
end with

WScript.Quit




