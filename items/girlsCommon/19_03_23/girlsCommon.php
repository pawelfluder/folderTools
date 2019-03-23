<?php

function Engine()
{
	CheckIfSaveBtnClicked();

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

function PrintPageView()
{
	//echo "page view</br>";
	echo "<form name='formularz1' method='post' action=''>
		<table>
			<tr>
				<td colspan=2>
					<b><h2>"
						.ReadFolderName()
					."<h2></b>
				</td>
			</tr>
			<tr>
				<td style='vertical-align: top'>
					<input type='submit' name='przycisk1' value='zapis1'><br/>
				</td>
				<td>
					<select name='select1'>
						<option>text</option>
						<option>folder</option>
					</select>
				</td>
			</tr>
			<tr>
				<td colspan=2>
					<textarea rows='1' cols='69' name='pole1'></textarea>
				</td>
			</tr>
			<tr>
				<td colspan=2 style='vertical-align: top'>";
					echo ReadGirlsFullNames();
				echo"</td>
			</tr>
		</table>
	</form>";
}

function ReadGirlsFullNames()
{
	$name = array();
	$i = 0;
	
	// jezeli potrafimy otworzyc odnosnik do bierzacego folderu
	if ($handle = opendir('.'))
	{		
		// doputy mamy kolejne pliki lub foldery
		while (false !== ($nam0 = readdir($handle))) 
		{
			if(IsCurrentDirectoryPointer($nam0) == false &&
				IsCurrentDirectoryPointer($nam0) == false &&
				IsPreviousDirectoryPointer($nam0) == false &&
				IsHiddenFileOrFolder($nam0) == false &&
				IsTwoDigitFolder($nam0))
			{
				$i++;
				$girlFullName = ReadGirlFullName($nam0);				
				$text = "<a href='$nam0'>$girlFullName</a>";
				
				$name[$i] = "$text</br>";

				
			}			
		}

		$len = sizeof($name);
		closedir($handle);
		
		sort($name);
		
		$toPrint = "";
		for ($i = 0; $i < $len; $i++){
			$toPrint .= $name[$i];
			$toPrint .= "\n";
		}
		
		return $toPrint;
	}
}

function ReadGirlFullName($folder)
{
	$cwd = getcwd();
	$filePath = $cwd.S.$folder.S."01".S."lista.txt";

	$Data = "";
	$Imie = "";
	$Nazwisko = "";
	$Numer = "";
	$i = 1;

	if(file_exists($filePath))
	{
		if ($file = fopen($filePath, "r")) 
		{
			while(!feof($file))
			{
				$line = stream_get_line($file, 200, "\n");

				if ($i >= 6 && $i <= 10)
				{										
					if ($i == 6)
					{
						$Data = $line;
					}
					if ($i == 7)
					{
						$Imie = $line;
					}
					if ($i == 8)
					{
						$Nazwisko = $line;
					}
					if ($i == 10)
					{
						$Numer = $line;
					}
				}				

				$i = $i +1;	
			}
			fclose($file);
		}
		//should be global var
		$U = "_";$U = "_";

		//$result = $Data.$U.$Imie.$U.$Nazwisko.$U.$Numer;
		$result = $Data.$Imie.$Nazwisko.$Numer;

		If($result == $U.$U.$U || $result == "")
		{
			$result = "Uzupelnij Info";
		}
		
		return $result;
	}

	//should be global var
	$E = "Nie ma pliku Info";
	return $E;
}


function CheckIfSaveBtnClicked()
{
	if (isset($_POST['przycisk1']))
	{
		if ($_POST['przycisk1'] == "zapis1"  ) 
		{
			WriteToFile();
		}
	}
}

function WriteToFile()
{
	$rootPath = ROOT;
	$src = $rootPath."templates".S."girls";

	$num = intval(LastFileNumber())+1;
	if ($num < 10)
	{
		$num = "0$num";
	}//echo "$num</br>";
		
	//mkdir("$num", 0777);
	copy_directory($src, "$num");
	
	$post = $_POST["pole1"];
	$filename = "$num/nazwa.txt";
	if(file_exists($filename)){
		$file = fopen($filename,"a+");
		fwrite($file, $post);
		fclose($file);
	}
	header("Refresh:0");
}

function CreateNameFile($num, $name)
{
	$filePath = "$num\\nazwa.txt";
	$myfile = fopen($filePath, "w");
	fwrite($myfile, $name);
	fclose($myfile);
}

function CreateListFile($num)
{
	$filePath = "$num\\lista.txt";
	$myfile = fopen($filePath, "w");
	fwrite($myfile, "\n\n\n\n");
	fclose($myfile);
}



?>