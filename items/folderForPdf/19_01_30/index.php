<?php
	if (session_status() != PHP_SESSION_ACTIVE) 
	{
		session_start();
	}
	OnStart();
	
	function GetRootPath()
	{
		$cwd = getcwd();
		
		$pubString = "public_html";
		if (\strpos($cwd, $pubString))
		{			
			$twoPathParts = explode($pubString, $cwd);
			return "$twoPathParts[0]$pubString";		
		}		
		return "/";
	}
	
	function OnStart()
	{
		$rootPath = GetRootPath();
		$commonPath = "$rootPath"."items/common/common.php";
		include "$commonPath";

		$typeName = "folderForPdf";
		$lastFolderPathForIndex = FindLastFolderPathForIndex($typeName, $rootPath);
		$lastFolderPathForIndex = "$lastFolderPathForIndex/index.php";
		$isUpToDate = IsIndexUpToDate($lastFolderPathForIndex);
		
		$lastFolderPathForCommon = FindLastFolderPathForCommon($typeName, $rootPath);
		include "$lastFolderPathForCommon";
		
		//View
		UpdateIndexIfNotUpToDate($isUpToDate, $lastFolderPathForIndex);
		EchoUpToDateStatement($isUpToDate);
		CheckIfSaveBtnClicked($rootPath);
	}
?>

<html>
<head>
	<title>
		<?php ReadFolderName()?>
	</title>
</head>

<body style="background-color: rgb(225,225,225)">
	<?php CheckPasswordAndPrintPageView() ?>
</body>
</html>
