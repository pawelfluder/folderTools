<?php

function Engine()
{
	echo
"<html>
<head>
	<title>
		".ReadFolderName()."
	</title>
</head>

<body style='background-color: rgb(225,225,225)'>
	".CheckPasswordAndPrintPageView()."
</body>
</html>";
}

function GetDownOrUpSetting()
{
	$filename = "lista.txt";
	if(file_exists($filename))
	{
		if ($file = fopen($filename, "r")) {
			if (($line = fgetss($file)) !== false)
			{
				$upDownLine = $line;
			}
		}
	}
	//echoVar("upDownLine", $upDownLine);
	
	$upString = "Up";
	$downString = "Down";
		
	if(strpos($upDownLine, $upString) !== false)
	{
		$upDownSetting = $upString;
	}
	else if(strpos($upDownLine, $downString) !== false)
	{
		$upDownSetting = $downString;
	}
	else
	{
		$upDownSetting = $upString;
	}
	//echoVar("upDnSetting", $upDnSetting);
	
	return $upDownSetting;
}

function printPageView()
{
	ReadPdfFile();
}

function ReadPdfFile()
{
	$pdf = FindPdfFiles();
	echo "<iframe src=\"$pdf\" width=\"100%\" style=\"height:100%\"></iframe>";
	//header("Location: $pdf");
}

function FindPdfFiles()
{
	$dir = '.';
	$files = scandir($dir,1);

	$i = 0;
	$pdfs = array();
	foreach($files as &$file)
	{
		if (substr($file,-4,4) == ".pdf")
		{
			$pdfs[$i] = $file;
			echo "$pdfs[$i].</br>";
			//echo "$file.</br>";
			$i++;
		}
	}
	$count = count($pdfs);
	if ($count > 0)
	{
		$pdf = $pdfs[0];
		return $pdf;
	}
	return "";	
}

function str_chop_lines($str, $lines = 4) 
{
    return implode("\n", array_slice(explode("\n", $str), $lines));
}

function GetTextToSave()
{
	$text = $_POST["pole1"];	
	return $text;
}

function SaveContent($filePath, $textToSave)
{
	$upDnSetting = GetDownOrUpSetting();
	
	if($upDnSetting == "Up")
	{
		$textToSave2 = "$upDnSetting\n\n\n\n$textToSave\n";
		$fileContent = file_get_contents($filePath);	
		$fileContent = str_chop_lines($fileContent);
		$textToSave2 .= $fileContent;
		$textToSave2 .= file_put_contents($filePath, $textToSave2);
	}
	else if($upDnSetting == "Down")
	{
		$file = fopen($filePath,"a+");			
		fwrite($file, "$textToSave\n");
		fclose($file);
	}
	//echoVar("upDnSetting", $upDnSetting);
}

?>