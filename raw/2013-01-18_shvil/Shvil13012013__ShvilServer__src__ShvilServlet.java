

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Servlet implementation class ShvilServlet
 */
@WebServlet(description = "server side of the shvil android application", urlPatterns = { "" })
public class ShvilServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public ShvilServlet() {
        super();
        // TODO Auto-generated constructor stub
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		PrintWriter out = response.getWriter();
		out.println("Yo Yo Yo!!!");
		out.println("This is so cool!!!!!!");
		out.println("Yo Yo Yo!!!");
		out.close();
		
		String action=request.getParameter("action");
        //EmployeeManager eMgr=new EmployeeManager();
		if(action == null)
		{
			action="";
        }
        else if(action.equals("getChangedRecords"))
        {
        	//Employee emp=eMgr.Populate(request);
	        try
	        {
	        	//eMgr.create(emp);
	        	System.out.println("Bla");
	        }
	        //catch(SQLException e)
	        //{
	        //	System.out.println("Exception="+e);
	        //}
	 
	        finally
	        {
	        	RequestDispatcher disp=request.getRequestDispatcher("/jsp/confirmation.jsp");
	        	disp.forward(request,response);
	        }
        }
        else if(action.equals("getUserID"))
        {
        	//Employee emp=eMgr.Populate(request);
	        try
	        {
	        	//eMgr.create(emp);
	        	System.out.println("Bla");
	        }
	        //catch(SQLException e)
	        //{
	        //	System.out.println("Exception="+e);
	        //}
	 
	        finally
	        {
	        	RequestDispatcher disp=request.getRequestDispatcher("/jsp/confirmation.jsp");
	        	disp.forward(request,response);
	        }
        }
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		//String action=request.getParameter("action");
        //EmployeeManager eMgr=new EmployeeManager();
		
		String requestBody = readInputStream(request.getInputStream());
				
		PrintWriter out = response.getWriter();
		out.write(requestBody);
		out.close();
        
	}
	
	private String readInputStream(InputStream in){
		BufferedReader bufferedReader = null;
		try 
		{
	        InputStreamReader reader = new InputStreamReader(in);
	        bufferedReader = new BufferedReader(reader);
	        StringBuilder builder = new StringBuilder();
	        String line = bufferedReader.readLine();
	        while (line != null) {
	            builder.append(line);
	            line = bufferedReader.readLine();
	        }
	        return builder.toString();
		} catch (Exception e) {
			e.printStackTrace();
		}
		finally
		{
			if (bufferedReader != null)
			{
				try
				{
					bufferedReader.close();
				}
				catch (IOException e)
				{
					e.printStackTrace();
				}
			}
		}
		return null;
    }

}
