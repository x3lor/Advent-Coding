public static class Input_20_23 {
 
    public static string example = 
@"broadcaster -> a, b, c
%a -> b
%b -> c
%c -> inv
&inv -> a";

    public static string example2 = 
@"broadcaster -> a
%a -> inv, con
&inv -> b
%b -> con
&con -> output";

    public static string input = 
@"%jb -> ps
%cm -> ps, tm
%sl -> ml, cp
%qr -> ml
%hf -> kh, jg
%jg -> kk
%jt -> pq
%qv -> kv
%rj -> mm, kh
%kf -> xt
%kx -> vk, mk
%dq -> qn
&ps -> xc, mq, jt, zs, sr, nt, pq
%jk -> hh, ps
%rr -> mk, nh
%hs -> kh, mb
%mg -> mk, kf
%xt -> dq, mk
&xc -> zh
%mq -> nt
%nh -> bm
&ml -> bp, gd, qv, kq
%md -> hs
%vk -> mk, vl
%mm -> kh
&th -> zh
&zh -> rx
%kc -> ps, jk
%kk -> dm
%jn -> ll, ml
&pd -> zh
&kh -> jg, qx, md, th, hf, dm, kk
%pp -> kh, md
%zf -> ml, bd
%qx -> pp
&mk -> kf, qn, nh, pd, dq, mg, bm
%qn -> rr
%mb -> qb, kh
%nt -> jt
%vl -> zk, mk
%gd -> ml, rm
%hh -> ps, jb
%tm -> ps, mq
%kv -> jn, ml
%zs -> kc
%ll -> ml, kq
%cp -> qv, ml
%rm -> sl, ml
%bd -> qr, ml
%dm -> qx
%qb -> rj, kh
%pq -> zs
%bm -> kx
%sr -> cm, ps
%zk -> mk
broadcaster -> sr, gd, mg, hf
%kq -> zf
&bp -> zh";
}