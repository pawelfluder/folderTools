<?php

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

function CheckIfSaveBtnClicked()
{
	$filePath = "lista.txt";
	if (isset($_POST['przycisk1']))
	{
		if ($_POST['przycisk1'] == "zapis1"  ) 
		{
			$textToSave = GetTextToSave();
			SaveContent($filePath, $textToSave);
		}
	}
}

function printPageView()
{
	//echo "printPageView()</br>";
	echo "<form name='formularz1' method='post' action=''>
		<table>
			<tr>
				<b><h2>"
					.ReadFolderName()
				."<h2></b>
			</tr>
			<tr>
				<td style='vertical-align: top'>";
					echo ReadFromFile();
				echo"</td>
			</tr>
		</table>
	</form>";
}

function ReadFromFile(){
	// jezeli potrafimy otworzyc odnosnik do bierzacego folderu
	if ($handle = opendir('.')) 
	{
		$name = array();
		// doputy mamy kolejne pliki lub foldery
		while (false !== ($nam0 = readdir($handle))) 
		{
			//echo "$nam0</br>";
			$begin = substr($nam0,0,4);//echo "$begin</br>";
			$end = substr($nam0,-4,4);//echo "$end</br>";

			if ($end == ".jpg")
			{
				$name[] = $nam0;			
			}
			if ($end == ".png")
			{
				$name[] = $nam0;			
			}				
		}
		closedir($handle);
		$count = count($name);
		if ($name > 0)
		{
			sort($name);
			for ($i = 0; $i < sizeof($name); $i++)
			{
				echo "<img src='$name[$i]'/></br>";
			}
		}		
	}
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