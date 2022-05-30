#! /usr/bin/env python
import os
python_v = '0'

try:
    # For Python 3.0 and later
    from urllib.request import urlopen
    python_v = '3'
except ImportError:
    # Fall back to Python 2's urllib2
    from urllib2 import urlopen
    python_v = '2'
try:
  import requests
except ImportError:
    if python_v == '2':
        print ("Trying to Install required module: Python2 requests\n")
        os.system('python -m pip install requests')
    elif python_v == '3':
        print ("Trying to Install required module: Python3 requests\n")
        os.system('pip3 install requests')

import requests
import json

api4 = 'http://api.vbox7.com/v4/?action='
token4 = '&app_token=imperia_android_0.1.0_3rG7jk'

if python_v == '3':
    print("Python 3")
    kodi_address = str(input("Enter Kodi IP-Address: "))
    kodi_username = str(input("Enter Kodi Username: "))
    kodi_password = str(input("Enter Kodi Password: "))
    kodi_json_request = str(input("Enter Kodi JSON_Qery: "))
else:
    print("Python 2")
    kodi_address = str(raw_input("Enter Kodi IP-Address: "))
    kodi_username = str(raw_input("Enter Kodi Username: "))
    kodi_password = str(raw_input("Enter Kodi Password: "))
    kodi_json_request = str(raw_input("Enter Kodi JSON_Qery: "))


url = 'http://' + kodi_address +'/jsonrpc'
headers={
    'Content-type':'application/json',
    'Accept':'application/json'
}


play_movie = {'jsonrpc':'2.0','id':'1','method':'Player.Open','params':{'item':{'file': url}}}
print("-----------------------------")
kodi_json_request = json.loads(kodi_json_request)
print (type(kodi_json_request))
print (kodi_json_request)
print("-----------------------------")
x = requests.post(url, json = play_movie, auth = (kodi_username, kodi_password), headers=headers)
print (x.status_code)
print (x.text)
