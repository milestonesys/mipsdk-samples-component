// MediaLiveServiceJPEG.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
#include <cassert>
#include <fstream>
#include <iostream>
#include <Toolkits/ToolkitInterface.h>
#include <Toolkits/ToolkitFactoryProvider.h>
#include <string>
#include <Tools/ServerCommandServiceClient.h>
#include <conio.h>
#include <ctime>

using namespace std;
using namespace NmToolkit;

using namespace NmServerCommandService;

bool login(ImServerCommandServiceClient *client, utf8_char_t** tokenPtr, size_t* tokenLength, NmServerCommandService::utc_time_t *registrationTime, NmServerCommandService::time_span_t *timeToLive)
{
	bool limited = false;

	std::wcerr << "... Trying to Login " << std::endl;
	// try login

	bool result = client->Login(tokenPtr, tokenLength, registrationTime, timeToLive, &limited);
	return result;
}


int _tmain(int argc, _TCHAR* argv[])
{
  ImData *data;
  ImToolkitFactory *factory;
  CmToolkitFactoryProvider provider;

  // GUID for the camera: CHANGE TO YOUR GUID
  utf8_string_t cameraGuid = "d3fba69e-fa92-4347-acd2-e216dcf35682";

  // NOTE: YOU NEED TO SET THESE NEXT 6 LINES!
  utf8_string_t vmsServer           = "localhost";             // The address you logon to
  short vmsServerPort               = 0;                       // ... and port - if 0, default will be used (80 for http, or 443 for https)
  utf8_string_t vmsRecorderUri      = "http://localhost:7563"; // The recorder where the camera is. Must be http:// or https://, depending on recorder configuration
  utf8_string_t username            = "";                      // Change to username (without domain)
  utf8_string_t password            = "";                      // Change to password
  AuthenticationMethod_t authMethod = WindowsAuthentication;   // Choose between BasicAuthentication or WindowsAuthentication
  // Note: If BasicAuthentication is selected, HTTPS will be used, then the port should (most likely) be 443 or 0. 
  
  // We catch all toolkit exceptions
  try
  {
		//------------------- Login and Token handling ---------------------

	    bool result;

		HMODULE module = LoadLibrary(L"ServerCommandServiceClient");
		if (module == NULL)
		{
			std::wcerr << "LoadLibrary failed with error code " << GetLastError() << std::endl;
			return EXIT_FAILURE;
		}

		CreateInstanceFuncPtr_t createInstanceFuncPtr = reinterpret_cast<CreateInstanceFuncPtr_t>(GetProcAddress(module, "CreateInstance"));
		DeleteInstanceFuncPtr_t deleteInstanceFuncPtr = reinterpret_cast<DeleteInstanceFuncPtr_t>(GetProcAddress(module, "DeleteInstance"));
		if (createInstanceFuncPtr == NULL)
		{
			std::wcerr << "GetProcAddress(CreateInstance) failed with error code " << GetLastError() << std::endl;
			return EXIT_FAILURE;
		}
		else if (deleteInstanceFuncPtr == NULL)
		{
			std::wcerr << "GetProcAddress(DeleteInstance) failed with error code " << GetLastError() << std::endl;
			return EXIT_FAILURE;
		}

		// Create client instance
		ImServerCommandServiceClient *client = NULL;
		client = createInstanceFuncPtr();
  
		// Setup connect details of the server command service
		client->SetServerHostName(vmsServer.c_str(), vmsServer.length());
		client->SetServerPort(vmsServerPort);
		client->SetUserName(username.c_str(), username.length());
		client->SetPassword(password.c_str(), password.length());
		client->SetAuthenticationMethod(authMethod);
		client->SetServerProduct(XProtectCorporateFamily);

	    utf8_char_t* tokenPtr = NULL;
		size_t tokenLength = 0;
		NmServerCommandService::utc_time_t registrationTime = 0;
		NmServerCommandService::time_span_t timeToLive = 0;

		result = login(client, &tokenPtr, &tokenLength, &registrationTime, &timeToLive);
		if (!result)
		{
			// The URL or Username/Password
			std::wcerr << "Unable to login to server" << std::endl;
			std::getchar();
			return EXIT_FAILURE;
		}

		const string token(tokenPtr, tokenLength);
		std::wcerr << "... Login Completed " << std::endl;

		utf8_string_t authorizationToken =           
						 "<authorization method='token'>"
						 "   <token update_key='token_key'>" + token + "</token>"
                         "</authorization>";


		utf8_string_t config = "<?xml version='1.0' encoding='utf-8'?>"
                         "<toolkit type='source'>"
                         "  <provider>mmp</provider>"
                         "  <config>"
                         "    <jpeg_encoder quality='90' quality_update_key='qual'>"
                         "      <video_decoder number_of_threads='4'>"
                         "        <toolkit type='source'>"
                         "          <provider>is</provider>"
                         "          <config>"
						 "            <server_uri>"+ vmsRecorderUri +"</server_uri>"
                         "            <device_id>"+ cameraGuid +"</device_id>"
                         "            <media_type>VIDEO</media_type>" + authorizationToken +
                         "            <maximum_queue_size>5</maximum_queue_size>"
                         "          </config>"
                         "        </toolkit>"
                         "      </video_decoder>"
                         "    </jpeg_encoder>"
                         "  </config>"
                         "</toolkit>";

	//--------------------- Media toolkit initialization ---------------

    // Create the toolkit factory
    factory = provider.CreateInstance();

    // Only continue if the toolkit factory was created successfully
    if (factory == 0)
      cerr << "Error creating toolkit factory!" << endl;
    else
    {
      // Create source toolkit instance
      ImToolkit *toolkit = factory->CreateInstance(config);

      // Downcast to source toolkit interface
      ImSourceToolkit *sourceToolkit = dynamic_cast<ImSourceToolkit *>(toolkit);

      // Verify that the toolkit created is a source toolkit as expected
      if (sourceToolkit == 0)
        cerr << "Created toolkit is not a Source Toolkit!" << endl;
      else
      {
		std::wcerr << "... Trying to Connect to the camera" << std::endl;

        // Connect to the source
        sourceToolkit->Connect();

        // Downcast to live source toolkit interface
        ImLiveSourceToolkit *liveSourceToolkit = dynamic_cast<ImLiveSourceToolkit *>(sourceToolkit);

        // Verify that the source toolkit created implements the live source toolkit interface
        if (liveSourceToolkit == 0)
          cerr << "Created source toolkit does not implement the live source toolkit interface!" << endl;
        else
        {
		  std::wcerr << "... Starting video stream" << std::endl;

          // Start live stream and initialize queue
          liveSourceToolkit->StartLiveStream();

          // We continue to get live data until the user stops the process by hitting a key on the keyboard or login fails.
		  // Note that error handling is very rudimentary in this example.
		  long ix = 1;
		  DWORD ftyp = GetFileAttributesA("C:\\Data");
		  if (ftyp == INVALID_FILE_ATTRIBUTES) {
			  CreateDirectory(L"C:\\Data", NULL);
		  }
          while (!_kbhit())
          {
            // Poll live queue waiting at most 10 seconds for data to arrive
            ImLiveSourceToolkit::get_live_status_t liveDataResult = liveSourceToolkit->GetLiveData(data, 10000);
            if (liveDataResult == ImLiveSourceToolkit::LIVE_DATA_RETRIEVED)
            {
              // Downcast to media data
              ImMediaData *mediaData = dynamic_cast<ImMediaData *>(data);
			  
              // Handle media data if that was what we retrieved
              if (mediaData != 0)
              {
				std::wcerr << "... Frame Received" << std::endl;
                // Write media data to file
				const byte* b1 = mediaData->GetMediaBodyPointer();
				size_t b2 = mediaData->GetMediaBodySize();				
                ofstream jpegFile("C:\\Data\\MyExportedImage"+std::to_string((long long)ix)+".jpg",ios::binary);
                jpegFile.write((const char *)mediaData->GetMediaBodyPointer(), mediaData->GetMediaBodySize());
                jpegFile.close();
				ix++;
                // We are done
				std::wcerr << "... Frame stored on disk" << std::endl;
				std::cerr << "." + to_string((long long)ix)+  "  ";
              }
			  
              // Delete data instance
              delete data;
            }
            else
            {
              cerr << "Getting data from live source toolkit timed out!" << endl;
            }

			// Compute the time to the when the token expires in seconds
			NmServerCommandService::utc_time_t expire_time_in_milliseconds = registrationTime + timeToLive;
			time_t expire_time_in_seconds = expire_time_in_milliseconds / 1000;

			// Find how many seconds are left before the token expires.
			// NOTE: This calculation is simplified and does not take into account if time(NULL) does not return the current time in UTC.
			// If it does not, you need to convert it to UTC.
			time_t current_time = time(NULL);
			time_t time_to_token_expires = expire_time_in_seconds - current_time;

			// Renew the token if it expires in less than 30 seconds.
			if (time_to_token_expires < 30)
			{
			  // Simply login again
			  result = login(client, &tokenPtr, &tokenLength, &registrationTime, &timeToLive);
			  if (!result)
			  {
			  	std::wcerr << "Unable to login to server" << std::endl;
			  	return EXIT_FAILURE;
			  }
			  
			  // Create the new token and update the toolkit configuration with the new token.
			  // We use the update key supplied in the original configuration.
			  const string token(tokenPtr, tokenLength);
			  sourceToolkit->UpdateConfiguration("token_key", token);
			}
          }

          // Stop live stream and clear the internal queue
          liveSourceToolkit->StopLiveStream();
        }

        // Disconnect from the source
        sourceToolkit->Disconnect();
      }

      // Delete source toolkit instance
      factory->DeleteInstance(toolkit);

	  // Verify the logout method
	  result = client->Logout();
	  
	  // Delete client instance
	  deleteInstanceFuncPtr(client);
	  		
	  if (!FreeLibrary(module))
	  {
	  	std::wcerr << "FreeLibrary failed with error code " << GetLastError() << std::endl;
	  	std::getchar();
	  	return EXIT_FAILURE;
	  }
    }
  }
  catch (const ImToolkitError &error)
  {
    // Write toolkit error to standard error output
    cerr << error.GetSpecificError() << endl;
  }

  // Delete the Toolkit factory
  provider.DeleteInstance(factory);

  std::wcerr << "Done... " << std::endl;
  std::getchar();

  // We are done
  return 0;
}
