<?php

function FindLastFolderPathForCommon()
{
	$folderPath = ROOT."items".S."common";
	$lastFolderPath = FindAlphabeticallyLastFolder($folderPath);	
	$lastFolderIndexPath = "$lastFolderPath".S."common.php";
	return $lastFolderIndexPath;
}

function FindAlphabeticallyLastFolder($folderPath)
{
	$pathList = (array) null;

	if ($handle = opendir($folderPath))
	{
		while (false !== ($current = readdir($handle))) 
		{
			if (IsCurrentDirectoryPointer($current) == false &&
				IsPreviousDirectoryPointer($current) == false &&
				IsHiddenFileOrFolder($current) == false)
			{
				$path = "$folderPath".S."$current";
				if (is_dir($path))
				{
					$pathList[] = $path;
				}
			}
		}	
	}
	
	$length = sizeOf($pathList);
	if ($length > 0)
	{
		sort($pathList);
		$alphabeticallyLastFolder = end($pathList);
		return $alphabeticallyLastFolder;
	}
	else
	{
		return null;
	}
}

function IsIndexUpToDate($lastFolderIndexPath)
{
	$libraryIndex = $lastFolderIndexPath.S.INDEX;
	$currentIndex = getcwd().S.INDEX;
	$isUpToDate = identical($libraryIndex, $currentIndex);

	return $isUpToDate;
}

function identical($fileOne, $fileTwo)
{
	if (filetype($fileOne) !== filetype($fileTwo)) return false;
	if (filesize($fileOne) !== filesize($fileTwo)) return false;
 
	if (! $fp1 = fopen($fileOne, 'rb')) return false;
 
	if (! $fp2 = fopen($fileTwo, 'rb'))
	{
		fclose($fp1);
		return false;
	}
 
	$same = true;
 
	while (! feof($fp1) and ! feof($fp2))
		if (fread($fp1, 4096) !== fread($fp2, 4096))
		{
			$same = false;
			break;
		}
 
	if (feof($fp1) !== feof($fp2)) $same = false;
 
	fclose($fp1);
	fclose($fp2);
 
	return $same;
}

function UpdateIndexIfNotUpToDate($isUpToDate, $lastFolderIndexPath)
{
	if ($isUpToDate == false)
	{
		$libraryIndex = $lastFolderIndexPath.S.INDEX;
		$currentIndex = getcwd().S.INDEX;
		copy($libraryIndex, $currentIndex);
	}
}

function IsCurrentDirectoryPointer($path)
{
	return $path == ".";
}

function IsPreviousDirectoryPointer($path)
{
	return $path == "..";
}

function IsHiddenFileOrFolder($path)
{
	return substr($path, 0, 1) == ".";
}

function IsTwoDigitFolder($file)
{
	$result = is_numeric($file);
	return $result;
}

?>