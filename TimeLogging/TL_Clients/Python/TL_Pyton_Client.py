import unittest
from unittest.mock import patch
from unittest.mock import Mock
import urllib.request
import urllib.parse
import json
import argparse
import http.client

class TimeLogClient:

    def __init__(self):
        self.host = "localhost"
        self.protocol = "http://"

    def DeleteTimeLog(self, port, tlid):
        self.hh = http.client.HTTPConnection(self.host, port)
        self.hh.request('DELETE', '/api/Logs/' + tlid)
        return self.hh.getresponse()

    def GetAllTimeLogs(self, port):
        url = self.protocol + self.host + ':' + port + '/api/Logs'
        self.urlReq = urllib.request.Request(url)
        return urllib.request.urlopen(self.urlReq)
        
class TimeLogClientTest(unittest.TestCase):

    def setUp(self):
        self.tlc = TimeLogClient()
        self.port = '50249'

    def test_GetAllTimeLogs_OneUrlReqMade(self):
        with patch.object(urllib.request, 'urlopen', return_value=None) as mock_method:
            self.tlc.GetAllTimeLogs(self.port)
        mock_method.assert_called_once_with(self.tlc.urlReq)

    def test_DeleteTimeLog_OneHttpDelMade(self):
        phony_text = bytes(json.dumps('phony HTTP response body'), 'UTF-8')
        phonyHttpConn = Mock(spec=http.client.HTTPConnection)
        phonyHttpConn.request = Mock()
        phonyHttpConn.getresponse = Mock()
        with patch.object(http.client, 'HTTPConnection', return_value=phonyHttpConn) as mock_method:
            self.tlc.DeleteTimeLog(self.port, '1')
        mock_method.assert_called_once_with(self.tlc.host, self.port)
        phonyHttpConn.request.assert_called_once()
        phonyHttpConn.getresponse.assert_called_once()

class TimeLogOps:

    def __init__(self):
        self.tlc = TimeLogClient()

    def GetAllTimeLogs(self, port):
        self.hr = self.tlc.GetAllTimeLogs(port)
        self._reportResponse()

    def DeleteTimeLog(self, port, tlid):
        self.hr = self.tlc.DeleteTimeLog(port, tlid)
        self._reportResponse()

    def _reportResponse(self):
        print(self.hr.status)
        body = self.hr.read()
        if (body != None):
            print(json.loads(str(body, "UTF-8")))

class TimeLogOpsTest(unittest.TestCase):

    def setUp(self):
        self.tlo = TimeLogOps()
        self.phony_text = bytes(json.dumps('phony HTTP response body'), 'UTF-8')
        self.phonyHttpResp = Mock(spec=http.client.HTTPResponse)
        self.phonyHttpResp.status = Mock(return_value=200)
        self.phonyHttpResp.read = Mock(return_value=self.phony_text)
        self.mockPrint = Mock(spec=print)
        self.port = '50249'

    def _assertSharedMocks(self):
        self.phonyHttpResp.status.assert_called_once()
        self.phonyHttpResp.read.assert_called_once()

    def test_GetAllTimeLogs_OneClientCallMade(self):
        with patch('builtins.print'):
            with patch.object(self.tlo.tlc, 'GetAllTimeLogs', return_value=self.phonyHttpResp) as mock_method:
                self.tlo.GetAllTimeLogs(self.port)
        mock_method.assert_called_once()
        self._assertSharedMocks()

    def test_DeleteTimeLog_OneClientCallMade(self):
        with patch('builtins.print'):
            with patch.object(self.tlo.tlc, 'DeleteTimeLog', return_value=self.phonyHttpResp) as mock_method:
                self.tlo.DeleteTimeLog(self.port, "1")
        mock_method.assert_called_once()
        self._assertSharedMocks()

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('port', help='port number for the targetted API')
    parser.add_argument('-id', help='the ID number of the Time Log entry to delete')
    args = parser.parse_args()
    tlo = TimeLogOps()
    if (args.id != None):
        tlo.DeleteTimeLog(args.port, args.id)
    else:
        tlo.GetAllTimeLogs(args.port)

if __name__ == '__main__':
    main()
