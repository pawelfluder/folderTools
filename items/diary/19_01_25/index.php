<?php
	$public = "public_html";
	$pwd = getcwd();
	$twoPathParts = explode($public, $pwd);
	$publicPath = "$twoPathParts[0]$public";
	
	$mostCommonFilePath = "$publicPath/items/common/common.php";
	include "$mostCommonFilePath";
	//echoVar("mostCommonFilePath", $mostCommonFilePath);
	
	CheckIfIndexIsUpToDate();
	
	$protectedFilePath = "$publicPath/items/protected.php";
	include "$protectedFilePath";
	//echoVar("protectedFilePath", $protectedFilePath);
	
	$protectedFilePath = "$publicPath/items/protected.php";
	include "$protectedFilePath";
	//echoVar("protectedFilePath", $protectedFilePath);
	
	function CheckIfIndexIsUpToDate()
	{
		$typeOfIndex = "text/";
		
		$public = "public_html";
		$pwd = getcwd();
		$twoPathParts = explode($public, $pwd);
		$folderPath = "$twoPathParts[0]$public/items/$typeOfIndex";
		
		$lastFolderPath = FindLastFolder($folderPath);
		$lastFolderIndexPath = $lastFolderPath."/index.php";
		$index = "index.php";
		$IsUpToDate = identical($lastFolderIndexPath, $index);
		
		//echoVar("folderPath", $folderPath);
		//echoVar("lastFolderIndexPath", $lastFolderIndexPath);
		//echoVar("pwd/gg.php", "$pwd/gg.php");
		//echoVar("IsUpToDate", $IsUpToDate);			
		if ($IsUpToDate)
		{
			echo "Index is up to date</br>";
		}
		else
		{
			echo "Index was updated, because was not up to date</br>";
			copy($lastFolderIndexPath, "$pwd/index.php");
		}	
	}
	
	function FindLastFolder($pathToSearch)
	{
		//echo "FindLastFolder()</br>";
		//echo "$pathToSearch</br>";
		if ($handle = opendir($pathToSearch))
		{
			// doputy mamy kolejne pliki lub foldery
			while (false !== ($fileOrFolderName = readdir($handle))) 
			{
				$path = $pathToSearch.$fileOrFolderName;
				if (is_dir($path))
				{
					if (substr($path, -1) != "." 
						&& substr($path, -2) != "..")
					{
						$pathList[] = $path;
					}			
				}
			}
			
			sort($pathList);
			/*for ($i = 0; $i < count($pathList); $i++)
			{
				echo "pathList[$i] = $pathList[$i]</br>";
			}*/
		}
		
		$lastFolderPath = end($pathList);
		//echoVar("lastFolderPath", $lastFolderPath);
		
		return $lastFolderPath;
	}
?>

<html>
<head>
	<title>
		<?php ReadFolderName()?>
	</title>
</head>

<body style="background-color: rgb(225,225,225)">
	<?php ApplyOnBtnClickScript() ?>
	<?php CheckPasswordAndPrintPageView() ?>
</body>
</html>

<?php


function GetTextToSave()
{
	//before time change
	//$time = date("H:i", strtotime('1 hour'));
	//after time change
	//$time = date("H:i", strtotime('2 hour'));
	
	$tempHourToCheck = date("H", strtotime('1 hour'));//echo "$hour</br>";
	if ($tempHourToCheck < "6") 
	{
		$date = date("d.m.Y", strtotime('- 6 hour'));
		$time = date("H:i", strtotime('1 hour'));//echo "Before 06:00";
    } 
	else
	{
		$date = date("d.m.Y");
		$time = date("H:i", strtotime('1 hour'));//echo "After 06:00";
	}
	return "$date;;$post;$time";
}

function ApplyOnBtnClickScript()
{
	$filePath = "lista.txt";
	$filePath2 = "../06/lista.txt";
	if (isset($_POST['przycisk1']))
	{
		if ($_POST['przycisk1'] == "zapis1"  ) 
		{
			$textToSave = GetTextToSave();
			SaveContent($filePath, $textToSave);
		}
	}
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
	
	//Problem: pierwsze dwa znaki było to
	//pytajniki, późnij już nie, więc tekst
	//się przesuwa
	
	//$threeToFive = substr($upDownLine,3,2);
	//echo "threeToFive: $threeToFive</br>";
	
	//$threeToSeven = substr($upDownLine,3,4);
	//echo "threeToSeven: $threeToSeven</br>";
		
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

function ReadFromFile(){
	$filename = "lista.txt";
	if(file_exists($filename))
	{
		$i = 0;
		if ($file = fopen($filename, "r")) {
			while(!feof($file))
			{
				$line = stream_get_line($file, 200, "\n");
				$i = $i +1;
				
				if ($i >= 5)
				{	
					$firstTwoChars= substr($line,0,2);
					$firstSevenChars= substr($line,0,7);
					$firstEightChars= substr($line,0,8);
					
					if (strpos($line, "\t") !== false) {
						$line = str_replace("\t", "&nbsp &nbsp &nbsp &nbsp", $line);
					}
									
					if($firstSevenChars == "http://")
					{
						echo "<a href='$line'>$line</a><br>";
					}
					else if($firstEightChars == "https://")
					{						
						echo "<a href='$line'>$line</a><br>";
					}
					else if($firstTwoChars == "//")
					{						
						echo "<font color='DarkSeaGreen'>".$line."</font><br>";
					}
					else if($firstTwoChars == "r/")
					{						
						echo "<font color='red'>".substr($line,2)."</font><br>";
					}
					else if($firstTwoChars == "g/")
					{						
						echo "<font color='green'>".substr($line,2)."</font><br>";
					}
					else if($firstTwoChars == "b/")
					{						
						echo "<font color='blue'>".substr($line,2)."</font><br>";
					}
					else
						{
							echo $line."<br>";
						}
					}
				}
				fclose($file);
			}
		else 
		{
			// error opening the file.
		}
	}
}

function str_chop_lines($str, $lines = 4) 
{
    return implode("\n", array_slice(explode("\n", $str), $lines));
}

?>