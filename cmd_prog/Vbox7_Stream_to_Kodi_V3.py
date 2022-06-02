#! /usr/bin/env python
import os
import time
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
        print ('Trying to Install required module: Python2 requests\n')
        os.system('python -m pip install requests')
    elif python_v == '3':
        print ('Trying to Install required module: Python3 requests\n')
        os.system('pip3 install requests')

import requests     
import json

api4 = 'http://api.vbox7.com/v4/?action='
token4 = '&app_token=imperia_android_0.1.0_3rG7jk'

if python_v == '3':
    print('Python 3')
    kodi_address = str(input('Enter Kodi IP-Address: '))
    kodi_username = str(input('Enter Kodi Username: '))
    kodi_password = str(input('Enter Kodi Password: '))
    url_id = str(input('Enter Vbox7 - URL: '))
else:
    print('Python 2')
    kodi_address = str(raw_input('Enter Kodi IP-Address: '))
    kodi_username = str(raw_input('Enter Kodi Username: '))
    kodi_password = str(raw_input('Enter Kodi Password: '))
    url_id = str(raw_input('Enter Vbox7 - URL:'))



if url_id[len(url_id)-1] == '#':
    url_id = url_id[:len(url_id)-1]


url = 'http://' + kodi_address +'/jsonrpc'
headers={
    'Content-type':'application/json',
    'Accept':'application/json'
}

# Clear Kodi playlist
clear_pl = {'jsonrpc': '2.0', 'id': 1, 'method': 'Playlist.Clear', 'params': {'playlistid': 1}}
x = requests.post(url, json = clear_pl, auth = (kodi_username, kodi_password), headers=headers)

# Inser item in play list 
create_pl = {'jsonrpc':'2.0','method':'Playlist.Insert','params':[1,0,{'file':'plugin://plugin.video.vbox7/?url=VOD&mode=14&name=%D0%97%D0%B0%D1%80%D0%B5%D0%B4%D0%B8+%D0%B2%D0%B8%D0%B4%D0%B5%D0%BE+%D0%BF%D0%BE+%D0%BD%D0%B5%D0%B3%D0%BE%D0%B2%D0%BE%D1%82%D0%BE+ID&video_id='+url_id+''}],'id':55}
x = requests.post(url, json = create_pl, auth = (kodi_username, kodi_password), headers=headers)

# Play video
inser_id = {'jsonrpc':'2.0','method':'Player.Open','params':{'item':{'position':0,'playlistid':1},'options':{}},'id':56}
x = requests.post(url, json = inser_id, auth = (kodi_username, kodi_password), headers=headers)