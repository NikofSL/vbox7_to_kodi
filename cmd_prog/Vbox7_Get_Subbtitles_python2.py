#! /usr/bin/env python
import os
python_v = '0'
import time

try:
    # For Python 3.0 and later
    from urllib.request import urlopen
    python_v = '3'
except ImportError:
    # Fall back to Python 2's urllib2
    from urllib2 import urlopen
    python_v = '2'

if python_v == '2':
    import urllib2   

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
import time  
import json

api4 = 'http://api.vbox7.com/v4/?action='
token4 = '&app_token=imperia_android_0.1.0_3rG7jk'

if python_v == '3':
    print("Python 3")
    url_id = str(input("Enter Vbox7 - URL: "))
else:
    print("Python 2")
    url_id = str(raw_input("Enter Vbox7 - URL:"))

try:
    if url_id[len(url_id)-1] == '#':
        url_id = url_id[:len(url_id)-1]
except:
    print("Please input URL: ")
url_id = url_id.split(":", 2)
url_id = str(url_id[2])
url_id = url_id.split("?", 1)
url_id = str(url_id[0])
d = api4 + 'r_video_play&video_md5=' + url_id + token4
discription = api4 + 'r_video_description&video_md5=' + url_id + token4
mybytes = urlopen(d).read()
mystr = mybytes.decode("utf8")
dbyte = urlopen(discription).read()
my_discription = dbyte.decode("utf8")
jsonrsp = json.loads('{'+mystr.split('{', 1)[-1])
jsonrsp2 = json.loads('{'+my_discription.split('{', 1)[-1])

title = jsonrsp['title'] + '.sub'

try:
	#Konvektirane na subs
	if not (jsonrsp['items'][0]['video_subtitles_path'] == ''):
		jsonrsp = json.loads(urllib2.urlopen(jsonrsp['items'][0]['video_subtitles_path']).read().split("var sSubsJson = '", 1)[-1].split("';", 1)[0].decode('utf8').encode('raw_unicode_escape').decode('utf-8', 'ignore').encode('utf-8', 'ignore').replace('\\\"', '"').replace('\\\\"', '\\"'))
		row = 0
		subs = ''
		for i in range(0, len(jsonrsp)):
			row = row + 1
			subs += str(row) +'\n'
			subs += time.strftime("%H:%M:%S,000", time.gmtime(int(jsonrsp[i]['f']))) + ' --> ' + time.strftime("%H:%M:%S,000", time.gmtime(int(jsonrsp[i]['t']))) + '\n'
			subs += jsonrsp[i]['s'].encode('raw_unicode_escape', 'ignore').decode('unicode_escape', 'ignore').encode('utf8', 'ignore').replace('+',' ').replace('|','').replace('<br>','\n')
			subs += '\n\n'
		st = 'true'
		with open(title, "w") as subfile:
			subfile.write(subs)
	else:
		st = 'false'
except:
	st = 'false'



print(type(jsonrsp))
print(jsonrsp)