import{A as o,u as r,B as n,a as c,o as p,b as u,e as i}from"./vendor.00b2d2ca.js";import{_ as d,v as l,b as m,l as _,P as v}from"./index.a845bb9f.js";const f={setup(){const e=o(),s=r();return n(async()=>{try{await l.getKeeps(e.params.id),await m.getById(e.params.id)}catch(a){s.push({name:"Home"}),_.log(a),v.toast(a.message)}}),{}}},g={class:"container"};function b(e,s,a,h,x,B){const t=c("VaultKeeps");return p(),u("div",g,[i(t)])}var $=d(f,[["render",b]]);export{$ as default};