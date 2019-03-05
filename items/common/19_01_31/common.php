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
		$hardcodedPassword = "66071805";
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

function RefreshPage()
{
	$cwd = getcwd();
	echo"
	<script type='text/javascript'>
	window.location.href = '$cwd';
	</script>";
}

function FindLastFolderPathForIndex()
{
	$folderPath = ROOT."items".S.TYPE;
	$lastFolderPath = FindAlphabeticallyLastFolder($folderPath);
	$lastFolderIndexPath = "$lastFolderPath";
	
	return $lastFolderIndexPath;
}

function FindLastFolderPathForTemplate()
{
	$templatePath = ROOT.S."items".S."templates".S.TYPE;
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
	if(file_exists($filename)){
		$file = fopen($filename, "r");
		$size = filesize($filename);
		if ($size > 0)
			$text = fread($file, $size);
		else
			$text = "";
		echo $text;
		fclose($file);
	}
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
		for ($i = 0; $i < $len; $i++){
			echo $name[$i];
		}
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
		echo "$LastFileNumber</br>";
		
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



