using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCL;

namespace ISL.Server.Common
{
	public static class ResourceManager
	{
		public static void initialize()
		{
			//PHYSFS_permitSymbolicLinks(1);

			//const std::string serverPath =
			//        Configuration::getValue("serverPath", ".");
			//const std::string clientDataPath =
			//        Configuration::getValue("clientDataPath", "example/clientdata");
			//const std::string serverDataPath =
			//        Configuration::getValue("serverDataPath", "example/serverdata");

			//PHYSFS_addToSearchPath(serverPath.c_str(), 1);
			//PHYSFS_addToSearchPath(clientDataPath.c_str(), 1);
			//PHYSFS_addToSearchPath(serverDataPath.c_str(), 1);
		}

		public static bool exists(string path)//, bool lookInSearchPath)
		{
			//if (!lookInSearchPath) return FileSystem.ExistsFile(path);
			return FileSystem.ExistsFile(path);
			//return PHYSFS_exists(path.c_str());

			return true; //ssk
		}

		static string resolve(string path)
		{
			//const char *realDir = PHYSFS_getRealDir(path.c_str());
			//if (realDir)
			//    return std::string(realDir) + "/" + path;

			//return std::string();

			return ""; //ssk
		}

		static byte[] loadFile(string fileName, int fileSize)
		{
			//// Attempt to open the specified file using PhysicsFS
			//PHYSFS_file *file = PHYSFS_openRead(fileName.c_str());

			//// If the handler is an invalid pointer indicate failure
			//if (file == NULL)
			//{
			//    LOG_WARN("Failed to load '" << fileName << "': "
			//             << PHYSFS_getLastError());
			//    return NULL;
			//}

			//// Get the size of the file
			//fileSize = PHYSFS_fileLength(file);

			//// Allocate memory and load the file
			//char *buffer = (char *) malloc(fileSize + 1);
			//if (PHYSFS_read(file, buffer, 1, fileSize) != fileSize)
			//{
			//    free(buffer);
			//    LOG_WARN("Failed to load '" << fileName << "': "
			//             << PHYSFS_getLastError());
			//    return NULL;
			//}

			//// Close the file and let the user deallocate the memory
			//PHYSFS_close(file);

			//// Add a trailing null character, so that the file can be used as a string
			//buffer[fileSize] = 0;
			//return buffer;

			return null; //ssk
		}

		static splittedPath splitFileNameAndPath(string fullFilePath)
		{
			//// We'll reversed-search for '/' or'\' and extract the substrings
			//// corresponding to the filename and the path separately.
			//size_t slashPos=fullFilePath.find_last_of("/\\");

			//ResourceManager::splittedPath splittedFilePath;
			//// Note the last slash is kept in the path name.
			//splittedFilePath.path=fullFilePath.substr(0, slashPos+1);
			//splittedFilePath.file=fullFilePath.substr(slashPos+1);

			//return splittedFilePath;

			return null; //ssk
		}
	}
}
