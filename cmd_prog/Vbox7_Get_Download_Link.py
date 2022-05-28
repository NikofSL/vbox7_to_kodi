#! /usr/bin/env python
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
print("#        www.vbox7.com")
if python_v == '3':
    url_id = str(input("Enter URL:"))
else:
    url_id = str(raw_input("Enter URL:"))

if url_id[len(url_id)-1] == '#':
    url_id = url_id[:len(url_id)-1]


api4 = 'http://api.vbox7.com/v4/?action='
token4 = '&app_token=imperia_android_0.1.0_3rG7jk'

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


#Zadavane na strim s optimalno kacestvo
try:
	stream = jsonrsp['items'][0]['video_location'].replace('.mpd','_720.mp4')
except:
	try:
		jsonrsp3 = json.loads(urllib2.urlopen('https://www.vbox7.com/ajax/video/nextvideo.php?vid=' + url).read())
		stream = jsonrsp3['options']['src'].replace('.mpd','_'+str(jsonrsp3['options']['highestRes'])+'.mp4')
	except:
		stream = 'http://m.vbox7.com/blank.mp4'

url_v = str(stream)


print("#    This is your Url-link to download the video file from www.vbox7.com")
print (url_v)
