var _BBLRTracker = new Object();
_BBLRTracker.cookie = new Object();
_BBLRTracker.utils = new Object();

_BBLRTracker.H = window.location.hostname;
_BBLRTracker.P = window.location.protocol||"http:";
_BBLRTracker.S = ["//t.babailiren.com/","d"];
_BBLRTracker.C = ["s","c","g","i","k"];
_BBLRTracker.CD= [["hmsr","utm_source"],["hmpl","utm_campaign"],["hmmd","utm_medium"],["hmci","utm_content"],["hmkw","utm_term"]];
_BBLRTracker.D = "";
_BBLRTracker.L = window.location.href + "";
_BBLRTracker.R = document.referrer + "";
_BBLRTracker.F = ["",""];
_BBLRTracker.E = "";
_BBLRTracker.K = "";
_BBLRTracker.W = "";
_BBLRTracker.Y = "";
_BBLRTracker.Z = "";
_BBLRTracker.A = "";
_BBLRTracker.T = "";
_BBLRTracker.J = "";
_BBLRTracker.Q = new Array();
_BBLRTracker.MSG = "";
_BBLRTracker.SE = false;
_BBLRTracker.FS = false;
if(typeof _BBLRPMP == "undefined" || _BBLRPMP == null || _BBLRPMP == ""){
	var _BBLRPMP = [[],[],[],[],[]];
};
if(typeof _BBLRCKD == "undefined" || _BBLRCKD == null || _BBLRCKD == ""){
	var _BBLRCKD = 30;
};
if(typeof _BBLRPPC == "undefined" || _BBLRPPC == null || _BBLRPPC == ""){
	var _BBLRPPC = [];
};
if(typeof _BBLRIFC == "undefined" || _BBLRIFC == null || _BBLRIFC == ""){
	var _BBLRIFC = false;
};
if(typeof _BBLRJID == "undefined" || _BBLRJID == null || _BBLRJID == ""){
	var _BBLRJID = "";
};
if(typeof _BBLRItems == "undefined" || _BBLRItems == null || _BBLRItems == ""){
	var _BBLRItems = new Array();
};
if(typeof _BBLRSID == "undefined" || _BBLRSID == null || _BBLRSID == ""){
	var _BBLRSID = "";
};
if(typeof _BBLRType == "undefined" || _BBLRType == null || _BBLRType == ""){
	var _BBLRType = "";
};
if(typeof _BBLRAmount == "undefined" || _BBLRAmount == null || _BBLRAmount == ""){
	var _BBLRAmount = "";
};
if(typeof _BBLRCurrency == "undefined" || _BBLRCurrency == null || _BBLRCurrency == ""){
	var _BBLRCurrency = "";
};
if(typeof _BBLROST == "undefined" || _BBLROST == null || _BBLROST == ""){
	var _BBLROST = true;
};
if(typeof _BBLRCFC == "undefined" || _BBLRCFC == null || _BBLRCFC == ""){
	var _BBLRCFC = new Array();
};
if(typeof _BBLRCCL == "undefined" || _BBLRCCL == null || _BBLRCCL == ""){
	var _BBLRCCL = [];
};
if(typeof _BBLRCMC == "undefined" || _BBLRCMC == null || _BBLRCMC == ""){
	var _BBLRCMC = new Array();
};
_BBLRTracker.o = function(data){
	var y = false;
	if((data.constructor+'').toLowerCase().match(/array/)){}else return "E01";
	if(data.length == 0) return "";
	var j = "";
	if(data.length > 0){
		for(var i=0;i<data.length;i++){
			if((data[i].constructor+'').toLowerCase().match(/array/)){}else return "E02";
			for(var f=0;f<data[i].length;f++){
				j += data[i][f].replace(/[\x7e;]/gi,'');
				if(f<data[i].length-1) j += ";";
			};
			if(i<data.length-1) j += "\x7e";
		};
	};
	
	return encodeURIComponent(j);
};

_BBLRTracker.i = function(){
	this.utils.p(this.L);
	
	this.D = this.cookie.a(this.H);
	_BBLRCMC[this.H] = ["direct","direct"];
	
	if(this.R == ""){
		this.F[0] = this.D;
		this.F[1] = this.H;
	}else{
		this.F[1] = this.utils.h(this.R);
		this.F[0] = this.cookie.a(this.F[1]);
		if(this.F[0] == this.D){
			this.F[0] = "";
			this.F[1] = "";
		};
	};
	
	this.FS = this.bes();
	this.W = _BBLRSID;
	this.T = _BBLRType;
	this.Y = _BBLRAmount;
	this.Z = _BBLRCurrency;
	this.J = _BBLRJID;
	if(this.J==""){}else this.cookie.s("_BBLR_CK","j",this.J,365, "/", this.D);
	this.m();
};

_BBLRTracker.m = function(){
	var _c_p = "";
	if(this.Q["__sn"]){
		_c_p = this.Q["__sn"];
	}else _c_p = this.F[1];
	
	if(typeof _BBLRCMC[_c_p] == "undefined" || _BBLRCMC[_c_p] == null || _BBLRCMC[_c_p] == ""){
	}else{
		if(_BBLRCMC[_c_p][1]=="ppc" && !this.FS){
			_c_p = "";
		}else if(_BBLRCMC[_c_p][1]=="direct"){
			_c_p = "direct";
		}else{
			_c_p = _BBLRCMC[_c_p][0];
		}
	}

	var _p = ["_BBLR_CK","e"];
	var val = this.cookie.r(_p[0],_p[1]); 
	
	if(val==""){
		if(_c_p != "") val = _c_p.toLowerCase();
	}else{
		var vals = val.split('~');
		if(vals.length > 0 && vals[0] == "") vals = [];
		if(vals.length > 0){
			var pos = vals.length - 1;
			if(vals[pos] != _c_p.toLowerCase() && _c_p != "") val += "~" + _c_p.toLowerCase();
		};
	};

	this.A = val;
	if(_BBLRIFC){
		this.cookie.s(_p[0],_p[1],"",365,"/", this.D);
	}else{
		if(val != ""){
			this.cookie.s(_p[0],_p[1],val,365, "/", this.D);
		};
	};
};

_BBLRTracker.u = function(){
	var _q = this.P + this.S[0] + this.S[1] + "?";
	var _n = "", _v="", _nn = "";
	for(var m=0;m<this.C.length;m++){
		_n = "__" + this.C[m] + "n";
		_v = (this.Q[_n])?this.Q[_n]:"";
		if(typeof _BBLRCMC[_v] == "undefined" || _BBLRCMC[_v] == null || _BBLRCMC[_v] == ""){
		}else{
			if(_BBLRCMC[_v][1] == "ppc" && !this.FS){
				_v = "";
			}
		}
		_q += this.C[m] + "=" + encodeURIComponent(_v) + "&";
	};
	
	if(this.J=="") this.J = this.cookie.r("_BBLR_CK","j");
	
	_q += "w=" + encodeURIComponent(this.W) + "&";
	_q += "t=" + encodeURIComponent(this.T) + "&";
	_q += "y=" + encodeURIComponent(this.Y) + "&";
	_q += "z=" + encodeURIComponent(this.Z) + "&";
	_q += "a=" + encodeURIComponent(this.A) + "&";
	_q += "d=" + encodeURIComponent(this.D) + "&";
	_q += "e=" + encodeURIComponent(this.E) + "&";
	_q += "m=" + encodeURIComponent(this.K) + "&";
	_q += "f=" + encodeURIComponent(this.F[0]) + "&";
	_q += "f1=" + encodeURIComponent(this.F[1]) + "&";
	_q += "j=" + encodeURIComponent(this.J) + "&";
	_q += "o=" + this.o(_BBLRItems) + "&";
	_q += "r=" + encodeURIComponent(this.R) + "&";
	_q += "b=" + encodeURIComponent(Math.round(Math.random()*10000000));
	return _q;
};

_BBLRTracker.v = function(){
	var curl = this.u();
	document.getElementById("_bblrIMG").src = curl;
};

_BBLRTracker.start = function(){
	this.i();
	var curl = this.u();
	this.cks();
	var el = "<div style='position:absolute;right:0px;top:0px;width:1px;height:1px;'/><img id='_bblrIMG' src='"+curl+"' width='1' height'1'/><input type='hidden' name='_debug' id='_debug' value=''/></div>";
	if(this.ose()) document.write(el);
	if(this.MSG != "") this.debug(this.MSG,true);
};

_BBLRTracker.event = function(code,str,isFinal,jid){
	if(code){}else code = "";
	if(str){}else str = "";
	if(jid){}else jid = "";
	if(code == "") return ;
	if(isFinal){
		this.W = str;
		this.m();
	};
	this.E = str;
	this.K = code;
	this.J = jid;
	if(this.J==""){}else this.cookie.s("_BBLR_CK","j",this.J,365, "/", this.D);
	this.v();
};

_BBLRTracker.bes = function(){
	var sec = false;
	var confs = [["m\\.baidu\\.com","word","wd"],["m[0-9]*\\.baidu\\.com","word","wd"],["www\\.baidu\\.com","word","wd"],["www\\.google\\.com","q"],["www\\.google\\.com\\.hk","q"],["www\\.google\\.cn","q"],["www\\.sogou\\.com","query"],["wap\\.sogou\\.com","keyword"],["www\\.zhongsou\\.com","w"],["search\\.yahoo\\.com","p"],["www\\.yahoo\\.cn","q"],["www\\.soso\\.com","w"],["wap\\.soso\\.com","key"],["search\\.114so\\.cn","kw"],["www\\.youdao\\.com","q"],["www\\.bing\\.com","q"],["m\\.bing\\.com","q"],["m\\.cn\\.bing\\.com","q"],["so\\.360\\.cn","q"],["m\\.so\\.com","q"],["www\\.so\\.com","q"]];
	for(var c=0;c<confs.length;c++){
		var re = new RegExp(confs[c][0]);
		if(re.test(this.F[1])){
			for(var w=1;w<confs[c].length;w++){
				var pe = new RegExp("[\?&]{1}"+confs[c][w]+"=");
				if(pe.test(this.R)){
					sec = true;
					break;
				};
			};
		};
		if(sec) break;
	};
	return sec;
};

_BBLRTracker.cks = function(){
	if(this.FS){
		if(this.ppc()){
			this.cookie.s("_BBLR_SE","ppc","true",_BBLRCKD, "/", this.D);
			this.cookie.s("_BBLR_SE","seo","false",_BBLRCKD, "/", this.D);
			this.cookie.s("_BBLR_SE","cps","false",_BBLRCKD, "/", this.D);
			this.cookie.s("_BBLR_SE","oac","false",_BBLRCKD, "/", this.D);
			this.SE = true;
		}else{
			this.cookie.s("_BBLR_SE","ppc","false",_BBLRCKD, "/", this.D);
			this.cookie.s("_BBLR_SE","seo","true",_BBLRCKD, "/", this.D);
			this.cookie.s("_BBLR_SE","cps","false",_BBLRCKD, "/", this.D);
			this.cookie.s("_BBLR_SE","oac","false",_BBLRCKD, "/", this.D);
			this.SE = true;
		};
	}else{
		var _v = (this.Q["__sn"])?this.Q["__sn"]:"";
		if(typeof _BBLRCMC[_v] == "undefined" || _BBLRCMC[_v] == null || _BBLRCMC[_v] == ""){
		}else{
			if(_BBLRCMC[_v][1] == "cps"){
				this.cookie.s("_BBLR_SE","ppc","false",_BBLRCKD, "/", this.D);
				this.cookie.s("_BBLR_SE","seo","false",_BBLRCKD, "/", this.D);
				this.cookie.s("_BBLR_SE","cps","true",_BBLRCKD, "/", this.D);
				this.cookie.s("_BBLR_SE","oac","false",_BBLRCKD, "/", this.D);
				this.SE = true;
			}else{
				if(_BBLRCMC[_v][1] == "ppc"){
					this.cookie.s("_BBLR_SE","ppc","false",_BBLRCKD, "/", this.D);
					this.cookie.s("_BBLR_SE","seo","false",_BBLRCKD, "/", this.D);
					this.cookie.s("_BBLR_SE","cps","false",_BBLRCKD, "/", this.D);
					this.cookie.s("_BBLR_SE","oac","true",_BBLRCKD, "/", this.D);			
				}else{
					this.cookie.s("_BBLR_SE","ppc","false",_BBLRCKD, "/", this.D);
					this.cookie.s("_BBLR_SE","seo","false",_BBLRCKD, "/", this.D);
					this.cookie.s("_BBLR_SE","cps","false",_BBLRCKD, "/", this.D);
					this.cookie.s("_BBLR_SE","oac","false",_BBLRCKD, "/", this.D);
				}
			};
		}
	};
};

_BBLRTracker.ppc = function(){
	var par = _BBLRPPC;
	if(par.length > 0 && par[0] instanceof Array){
		for(var m=0;m<par.length;m++){
			for(var mn=1;mn<par[m].length;mn++){
				if(this.Q["__sn"] && this.Q["__sn"] == par[m][mn]){
					return true;
				};
			}
		}
	}else{
		for(var m=0;m<par.length;m++){
			if(this.Q["__sn"] && this.Q["__sn"] == par[m]){
				return true;
			};
		};
	}
	return false;
};

_BBLRTracker.ose = function(){
	if(_BBLROST){
		var seov = this.cookie.r("_BBLR_SE","seo");
		var seop = this.cookie.r("_BBLR_SE","ppc");
		var seos = this.cookie.r("_BBLR_SE","cps");
		if(this.SE || seov == "true" || seop == "true" || seos == "true" || !(this.K == "")){
			return true;
		}else{
			return false;
		}
	}else{
		return true;
	}
};

_BBLRTracker.debug = function(msg,show){
	if(show){
		document.getElementById("_debug").value = this.MSG;
	}else{
		this.MSG = this.MSG +" / " + msg;
	};
};

_BBLRTracker.utils.h = function(u){
	if(u.toLowerCase().indexOf("http") != 0) return "";
	var re=/^http[s]*:\/\/([^\/]*)?/gi;
	re.test(u);
	return RegExp.$1; 
};

_BBLRTracker.utils.decode = function(str){
	var cstr = str;
	if(str.indexOf("%u") > -1){
		cstr = unescape(str);
	}else{
		cstr = decodeURIComponent(str);
	};
	return cstr;
};

_BBLRTracker.utils.p = function(u){
	_BBLRTracker.Q = this.pa(u);
	var _n = "", _v="", _nn = "",_mr=false;
	for(var m=0;m<_BBLRTracker.C.length;m++){
		_n = "__" + _BBLRTracker.C[m] + "n";
		_v = (_BBLRTracker.Q[_n])?_BBLRTracker.Q[_n]:"";
		if(_v==""){
			if(_BBLRPMP[m].length > 0){
				for(var cn=0;cn<_BBLRPMP[m].length;cn++){
					_nn = _BBLRPMP[m][cn];
					if(_BBLRTracker.Q[_nn]){
						_v = _BBLRTracker.Q[_nn];
						_BBLRTracker.Q[_n] = _v;
						_mr = true;
						break;
					};
				};
			}else if(_BBLRTracker.CD[m].length > 0){
				for(var cn=0;cn<_BBLRTracker.CD[m].length;cn++){
					_nn = _BBLRTracker.CD[m][cn];
					if(_BBLRTracker.Q[_nn]){
						_v = _BBLRTracker.Q[_nn];
						_BBLRTracker.Q[_n] = _v;
						_mr = true;
						break;
					};
				};
			}	
		};	
	}
	if(_mr){}else{
		var _mr = false;
		for(var r1=0;r1<_BBLRCCL.length;r1++){
			var r1n=_BBLRCCL[r1];
			for(var r2=0;r2<_BBLRCFC[r1n].length;r2++){
				var r2n=_BBLRCFC[r1n][r2];

				if(_BBLRCMC[r2n][2]==""){
				}else{
					var prns = _BBLRCMC[r2n][2].split(",");
					for(var r4=0;r4<prns.length;r4++){
						var cexp = new RegExp(prns[r4]);
						if(cexp.test(_BBLRTracker.L)){
							_mr = true;
							_BBLRTracker.Q["__sn"] =  _BBLRCMC[r2n][0];
						}
						if(_mr) break;
					}
				}

				if(_mr) break;
			}
			if(_mr) break;
		}
	}
};

_BBLRTracker.utils.pa = function(u){
	if(u.toLowerCase().indexOf("http") != 0) return "";
	var x = u.indexOf("#");
	var y = u;
	if(x > -1) y = u.substring(0,x);
	x = y.indexOf("?");
	var z = (x>-1)?y.substring(x+1,y.length):"";
	var _a,_m,_c,_v;
	_a = z.split("&");
	if(_a.length > 0 && _a[0]=="") _a = [];
	var reqs = new Array();
	for(var _d=0;_d<_a.length;_d++){
		_m = _a[_d].split("=");
		_c = _m[0];
		_v = (_m.length == 2)?_m[1]:"";
		reqs[_c] = this.decode(_v);
	};
	return reqs;
};

_BBLRTracker.utils.trim = function(str){
	var lstr = str.replace( /^\s*/, ""); 
	var rstr = lstr.replace( /\s*$/, ""); 
	return rstr;
};

_BBLRTracker.cookie.DT = ["com","net","edu","org","gov","biz","coop","info","aero","pro","name","museum","int","arpa"];
_BBLRTracker.cookie.g = function(cname){
	var allcookies = document.cookie;
	if (allcookies == "") return [];
	var cookies = allcookies.split(';');
	var tcookie = [];
	if(cookies.length > 0 && cookies[0] == "") cookies = [];
	for(var i = 0; i < cookies.length; i++) {
		var pos = cookies[i].indexOf('=');
		var ck_n = _BBLRTracker.utils.trim(cookies[i].substring(0,pos));
		var ck_v = _BBLRTracker.utils.trim(cookies[i].substring(pos+1,cookies[i].length));
		tcookie = [ck_n,ck_v];
		if (tcookie[0] == cname) {
			return tcookie;
		};
	};
	return [];
};

_BBLRTracker.cookie.p = function(ccookie){
	var cookieval = (ccookie.length==2)?decodeURIComponent(ccookie[1]):"";
	var a = cookieval.split('&');
	if(a.length==1 && a[0] == "") a = [];

	var v = new Array();
	for(var i=0; i < a.length; i++){
		v[i] = a[i].split(':');
	};
	return [a,v];
};

_BBLRTracker.cookie.r = function(cname,subname){
	var cookie = this.g(cname);
	var ar = this.p(cookie);
	for(var i = 0; i < ar[0].length; i++) {
		if(ar[1][i][0] == subname) return decodeURIComponent(ar[1][i][1]);
	};
	return "";
};

_BBLRTracker.cookie.s = function(cname,subname,subval,daysToLive, path, domain){
	var cookie = this.g(cname);
	var ar = this.p(cookie);

	var find = false;
	for(var i = 0; i < ar[0].length; i++) {
		if(ar[1][i][0] == subname){
			ar[1][i][1] = encodeURIComponent(subval);
			find = true;
		};
	};

	var cnkv = "";
	for(var i = 0; i < ar[0].length; i++) {
		cnkv += ar[1][i][0]+":"+ar[1][i][1];
		if(i<ar[0].length-1) cnkv += "&";
	};

	if(find){}else{
		if(ar[0].length>0) cnkv += "&";
		cnkv += subname+":"+encodeURIComponent(subval);
	};

	this.e(cname,cnkv,daysToLive, path, domain);
};

_BBLRTracker.cookie.d = function(cname,subname,daysToLive, path, domain){
	if(subname=="") return ;
	var cookie = this.g(cname);
	var ar = this.p(cookie);

	var nkv = "";
	var fi = 0;
	for(var i = 0; i < ar[0].length; i++) {
		if(ar[1][i][0] == subname){
			fi = i;
		}else{
			nkv += ar[1][i][0]+":"+ar[1][i][1];
			if(i<ar[0].length - 1) nkv += "&";
		};
	};
	var fkv = nkv;
	if(nkv.charAt(nkv.length-1) == '&') fkv = nkv.substring(0,nkv.length - 1);

	this.e(cname,fkv,daysToLive, path, domain);
};

_BBLRTracker.cookie.e = function(cname,cval,daysToLive, path, domain){
	var cookie = cname + '=' + encodeURIComponent(cval);
	if (daysToLive || daysToLive != 0) {
		cookie += "; max-age=" + (daysToLive*24*60*60);
	};

	if (path) cookie += "; path=" + path;
	if (domain) cookie += "; domain=." + domain;
	document.cookie = cookie;
};

_BBLRTracker.cookie.a = function(host){
	var profix = this.DT;
	var pairs = host.split(".");
	var p = pairs.length - 1;
	var val = parseInt(pairs[p]);
	var domain = "";
	if(isNaN(val)){
		var sec = pairs[p-1].toLowerCase();
		var inscope = false;
		for(var c=0;c<profix.length;c++){
			if(sec == profix[c]){
				inscope = true;
				break;
			};
		};
		if(inscope){
			domain = (p>2)?pairs.slice(p-2,p+1).join("."):pairs.join(".");
		}else{
			domain = (p>1)?pairs.slice(p-1,p+1).join("."):pairs.join(".");
		};
	}else{
		domain = host;
	};
	return domain;
 };
document.write(unescape("%3Cscript src='" + _BBLRTracker.P + _BBLRTracker.S[0] + "cc/" + _BBLRTracker.cookie.a(_BBLRTracker.H)+".js' type='text/javascript'%3E%3C/script%3E"));