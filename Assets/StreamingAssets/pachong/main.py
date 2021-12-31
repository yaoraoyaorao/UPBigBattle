import json
import requests
import time
import os
import sys

os.environ['NO_PROXY']='https://api.bilibili.com'

# https://api.bilibili.com/x/relation/followings?vmid=394736064&pn=1&ps=20&order=desc
#mid = 394736064
mid = sys.argv[1]
ps = 20
#cookie = sys.argv[2]
head = {
    "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36",
    }
def following_url(pn):
    return 'https://api.bilibili.com/x/relation/followings?vmid='+ str(mid) +'&pn='+str(pn)+'&ps='+str(ps)+'&order=desc'
def follower_url(id):
    return 'https://api.bilibili.com/x/relation/stat?jsonp=jsonp&vmid={}'.format(id)
def get_followerNum(id):
    return json.loads(requests.get(follower_url(id)).text)['data']['follower']

def getData():
    upList = []
    updic={}
    r = requests.get(following_url(1),headers=head)
    for i in json.loads(r.text)['data']['list']:
        upInfo={}
        upInfo['ID'] = i['mid']
        upInfo['follower'] = get_followerNum(i['mid'])
        upInfo['Name'] = i['uname']
        upInfo['face'] = i['face']
        upInfo['sign'] = i['sign']
        print(upInfo)
        upList.append(upInfo)
    print(upList)
    updic['list']=upList
    with open(sys.argv[2], 'w') as f:
    #with open("data.json", 'w') as f:
        json.dump(updic, f)
    print('success Save')


if __name__ == "__main__":
    getData()
