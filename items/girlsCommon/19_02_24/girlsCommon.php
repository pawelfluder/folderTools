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
					echo ReadFoldersName();
				echo"</td>
			</tr>
		</table>
	</form>";
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
	$src = $rootPath.S."templates".S."girls";

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