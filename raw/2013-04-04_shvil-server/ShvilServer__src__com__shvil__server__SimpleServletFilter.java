package com.shvil.server;

import javax.servlet.Filter;
import javax.servlet.FilterChain;
import javax.servlet.FilterConfig;
import javax.servlet.ServletException;
import javax.servlet.ServletRequest;
import javax.servlet.ServletResponse;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**

 */
public class SimpleServletFilter implements Filter {

    public void init(FilterConfig filterConfig) throws ServletException {
    }

    public void doFilter(ServletRequest req, ServletResponse resp, FilterChain chain) throws ServletException, java.io.IOException {
    HttpServletRequest request = (HttpServletRequest) req;
    HttpServletResponse response = (HttpServletResponse) resp;

    // This should be added in response to both the preflight and the actual request
    response.addHeader("Access-Control-Allow-Origin", "*");

    if ("OPTIONS".equalsIgnoreCase(request.getMethod())) {
        response.addHeader("Access-Control-Allow-Methods", "GET, POST");
    	response.addHeader("Access-Control-Allow-Headers", "action, record_type, user_id, max_server_time");
    }

    chain.doFilter(req, resp);
}

    public void destroy() {
    }
}