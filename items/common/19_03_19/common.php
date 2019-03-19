<?php

function FindLastFolderPathForTypeCommon()
{
	$folderPath = ROOT."items".S.TYPE."Common";	
	$lastFolderPath = FindAlphabeticallyLastFolder($folderPath);	
	$lastFolderIndexPath = "$lastFolderPath".S.TYPE."Common.php";
	return $lastFolderIndexPath;
}

//tutaj są tylko funkcje, które są koniecznie do index update
function echoVar($name, $var)
{
	$fileType = gettype ($var);
	if ($fileType == "string")
	{
		echo "$name: \"$var\"</br>";
	}
	if ($fileType == "boolean")
	{
		if ($var)
		{
			echo "$name: \"true\"</br>";
		}
		else
		{
			echo "$name: \"false\"</br>";
		}	
	}
}

function PrintPasswordForm()
{
	echo "<form method=post>
			<input type='password' name='pass'></br>
			<input type='submit' value='Zaloguj'>
		</form>";
}

function CheckPasswordAndPrintPageView()
{
	//hasło sesji
		if(isset($_SESSION['session_pass']))
		{
			$sessionPass = $_SESSION['session_pass'];
		}
		else
		{
			$sessionPass = "";
		}
	//haslo wpisane do textbox'a
		if(isset($_POST['pass']))
		{
			$passTypedToTextBox = $_POST['pass'];
		}
		else
		{
			$passTypedToTextBox = "";
		}
	//wpisywanie haslo z textbox do sesji
		if ($passTypedToTextBox)
		{
			$_SESSION['session_pass'] = $passTypedToTextBox;
			RefreshPage();
		}
	//sprawdzenia hasła
		$hardcodedPassword = GetPassword();
		$isPasswordCorrect = ($hardcodedPassword == $sessionPass);

	//wyswietlenie formularza z haslem lub strony
	if ($isPasswordCorrect == true)
	{
		PrintPageView();
	}
	else
	{
		PrintPasswordForm();
	}
}

function GetPassword()
{
	$categoryFolderPath = GetCategoryFolderPath();
	$categoryPasswordPath = $categoryFolderPath.S."3d19ef96-adb0-4ecb-a385-827a2857dc55.txt";
	$pass = ReadFirstLine($categoryPasswordPath);
	
	return $pass; 
}

function GetCategoryFolderPath()
{
	$cwd = getcwd();
	$root = ROOT;
	
	$explode1 = explode($root,$cwd);
	$category = explode(S,$explode1[1]);

	return ROOT.$category[0];
}

function RefreshPage()
{
	$cwd = getcwd();
	echo"
	<script type='text/javascript'>
	window.location.href = '$cwd';
	</script>";
}

function ReadFirstLine($path)
{
	$line = "";
	if(file_exists($path))
	{
		$file = fopen($path, "r");
		$line = fgets($file);
		fclose($file);
	}
	return $line;
}

function FindLastFolderPathForIndex()
{
	return FindLastFolderPathForType(TYPE);
}

function FindLastFolderPathForType($type)
{
	$folderPath = ROOT."items".S.$type;
	$lastFolderPath = FindAlphabeticallyLastFolder($folderPath);
	$lastFolderIndexPath = "$lastFolderPath";
	
	return $lastFolderIndexPath;
}

function FindLastFolderPathForTemplate()
{
	$templatePath = ROOT.S."templates".S.TYPE;
	return $templatePath;
}

function EchoUpToDateStatement($isUpToDate)
{
	if ($isUpToDate == true)
	{
		echo "Index was up to date</br>";
	}
	else
	{
		echo "Index was updated, because was not up to date</br>";
	}
}

function ReadFolderName()
{
	$filename = "nazwa.txt";
	$text = "";
	if(file_exists($filename))
	{
		$file = fopen($filename, "r");
		$size = filesize($filename);
		if ($size > 0)
		{
			$text = fread($file, $size);
		}
		fclose($file);
	}
	return $text;
}

function ReadFoldersName()
{
	$name = array();
	
	// jezeli potrafimy otworzyc odnosnik do bierzacego folderu
	if ($handle = opendir('.')) {
		
		$i = 0;
		// doputy mamy kolejne pliki lub foldery
		while (false !== ($nam0 = readdir($handle))) 
		{
			$filename = "$nam0/nazwa.txt";
			if(IsCurrentDirectoryPointer($nam0) == false &&
				IsCurrentDirectoryPointer($nam0) == false &&
				IsPreviousDirectoryPointer($nam0) == false &&
				IsHiddenFileOrFolder($nam0) == false &&
				file_exists($filename))
			{
				$file = fopen($filename, "r");
				$size = filesize($filename);
				$i++;
				if ($size > 0)
				{				
					$text = "$nam0 <a href='$nam0'>".fread($file, $size)."</a>";
					$name[$i] = "$text</br>";		
				}								
				else
				{
					$name[$i] = "$i</br>";
				}				
				
				fclose($file);
			}
			else
			{
				$i++;
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

function LastFileNumber()
{
	// jezeli potrafimy otworzyc odnosnik do bierzacego folderu
	if ($handle = opendir('.')) 
	{
		$i = 0;
		// doputy mamy kolejne pliki lub foldery
		while (false !== ($nam0 = readdir($handle))) 
		{
			if(is_dir($nam0))
			{
				$num[$i]=$nam0;
				$i++;
			}
		}
		closedir($handle);
		
		sort($num);
		$LastFileNumber = checkIfFolderNameIsANumber($num);
		
		return $LastFileNumber;
	}
}

function checkIfFolderNameIsANumber($num)
{
	//*
	for ($i=sizeof($num)-1;$i>0;$i--)
	{
		//echo "pętla: $i $num[$i]</br>";
		if(intval($num[$i]) != "0")
		{
			break;
			//echo "$i $num[$i] is_a_number</br>";
		}
		else
		{
			//echo "$i $num[$i] is_not_a_number</br>";
		}
	}	
	return $num[$i];
}

function copy_directory($src,$dst) { 
    $dir = opendir($src); 
    @mkdir($dst); 
    while(false !== ( $file = readdir($dir)) ) { 
        if (( $file != '.' ) && ( $file != '..' )) { 
            if ( is_dir($src . S . $file) ) { 
                copy_directory($src . S . $file,$dst . S . $file); 
            } 
            else { 
                copy($src . S . $file,$dst . S . $file); 
            } 
        } 
    } 
    closedir($dir);
}

?>



